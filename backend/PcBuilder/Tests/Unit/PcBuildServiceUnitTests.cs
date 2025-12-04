using AutoMapper;
using BLL.App.Services;
using BLL.DTO;
using BLL.DTO.Mappers;
using BLL.DTO.PcBuild;
using DAL.EF.App;
using DAL.EF.App.Seeding;
using DAL.EF.App.Seeding.Names;
using Domain.App;
using Microsoft.EntityFrameworkCore;
using Tests.Helpers;

namespace Tests.Unit;

public class PcBuildServiceUnitTests
{
    private readonly ApplicationDbContext _context;
    private readonly PcBuildService _service;
    
    private readonly Dictionary<string, Guid> _componentIds = new();

    private const string TestPcName = "Test PC";
    private const string TestPcDesc = "Test Description";
    private const string UpdatedPcName = "Updated PC";
    private const string UpdatedPcDesc = "Updated Description";
    private const int Stock = 100;
    private const decimal Price = 100m;
    
    private readonly Guid _discountId = Guid.NewGuid();
    private readonly Guid _manufacturerId = Guid.NewGuid();

    private readonly List<string> _componentCategories = new()
    {
        CategoryNames.Case,
        CategoryNames.Motherboard,
        CategoryNames.Processor,
        CategoryNames.CpuCooler,
        CategoryNames.Memory,
        CategoryNames.GraphicsCard,
        CategoryNames.SolidStateDrive,
        CategoryNames.HardDrive,
        CategoryNames.PowerSupply,
        CategoryNames.OperatingSystem
    };

    private readonly List<string> _pcCategories = new()
    {
        CategoryNames.TemplatePc,
        CategoryNames.CustomPc,
        CategoryNames.PrebuiltPc
    };
    
    public PcBuildServiceUnitTests()
    {
        // set up mock database - in memory
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

        // use random guid as db instance id
        optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
        _context = new ApplicationDbContext(optionsBuilder.Options);

        // reset db
        _context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();
        
        // set up automapper configurations
        var bllProfile = new AutoMapperConfig();
        var dalProfile = new DAL.DTO.AutoMapperConfig();
        
        var configuration = new MapperConfiguration(config =>
        {
            config.AddProfile(bllProfile);
            config.AddProfile(dalProfile);
        });

        // initialize automapper
        var mapper = new Mapper(configuration);

        // initialize uow
        var uow = new AppUOW(_context, mapper);
        
        // initialize service
        _service = new PcBuildService(
            uow, new PcBuildMapper(mapper), new PcComponentMapper(mapper), new UserReviewMapper(mapper));
    }
    
    [Theory(DisplayName = "AllAsync method with all PC categories - 0, 1, and 10 elements each")]
    [ClassData(typeof(TestDataGenerator))]
    public async Task TestPcBuildServiceAllAsync(int count, string pcCategory)
    {
        await SeedTestDataAsync(count, pcCategory);
        
        var result = (await _service.AllAsync()).ToList();
        
        Assert.NotNull(result);
        Assert.IsType<List<PcBuildDTO>>(result);
        Assert.Equal(count, result.Count);
        
        if (count > 0)
        {
            var first = result.First();
            
            Assert.Equal(TestPcName, first.PcName);
            Assert.Equal(pcCategory, first.CategoryName);
            Assert.Equal(1000, first.Cost);
        }
    }

    [Theory(DisplayName = "AllAsyncStore method with all PC categories - 0, 1, and 10 elements each")]
    [ClassData(typeof(TestDataGenerator))]
    public async Task TestPcBuildServiceAllAsyncStoreCorrectCategory(int count, string pcCategory)
    {
        await SeedTestDataAsync(count, pcCategory);
        
        var result = (await _service.AllAsyncStore()).ToList();
        
        Assert.NotNull(result);
        Assert.IsType<List<PcBuildStoreDTO>>(result);
        Assert.Equal(pcCategory == CategoryNames.PrebuiltPc ? count : 0, result.Count);

        if (pcCategory == CategoryNames.PrebuiltPc && count > 0)
        {
            var first = result.First();
            
            Assert.Equal(TestPcName, first.PcName);
            Assert.Equal(10, first.PcComponents.Count);
        }
    }

    [Fact(DisplayName = "FindAsyncDetails method")]
    public async Task TestPcBuildServiceFindAsyncDetails()
    {
        var id = await SeedTestDataAsyncWithId();

        var result = await _service.FindAsyncDetails(id);
        
        Assert.NotNull(result);
        Assert.IsType<PcBuildDetailsDTO>(result);
        Assert.Equal(TestPcName, result.PcName);
        Assert.Equal(10, result.PcComponents.Count);

        foreach (var component in result.PcComponents)
        {
            Assert.Contains(_componentCategories, c => c == component.CategoryName);
        }
    }
    
    [Fact(DisplayName = "FindAsyncDetails method with invalid id")]
    public async Task TestPcBuildServiceFindAsyncDetailsInvalidInput()
    {
        await SeedTestDataAsync();

        var result = await _service.FindAsyncDetails(Guid.Empty);
        
        Assert.Null(result);
    }
    
    [Fact(DisplayName = "FindAsyncEdit method")]
    public async Task TestPcBuildServiceFindAsyncEdit()
    {
        var id = await SeedTestDataAsyncWithId();

        var result = await _service.FindAsyncEdit(id);
        
        Assert.NotNull(result);
        Assert.IsType<PcBuildEditDTO>(result);
        Assert.Equal(TestPcName, result.PcName);
        Assert.NotEmpty(result.CaseId.ToString());
    }
    
    [Fact(DisplayName = "FindAsyncEdit method with invalid id")]
    public async Task TestPcBuildServiceFindAsyncEditInvalidInput()
    {
        await SeedTestDataAsync();

        var result = await _service.FindAsyncEdit(Guid.Empty);
        
        Assert.Null(result);
    }
    
    [Fact(DisplayName = "Update method without changing anything")]
    public async Task TestPcBuildServiceUpdateNoChanges()
    {
        var id = await SeedTestDataAsyncWithId();

        var dto = GetEditDto();

        dto.Id = id;
        
        var result = await _service.Update(dto);

        Assert.NotNull(result);
        Assert.IsType<PcBuildEditDTO>(result);
        
        Assert.Equivalent(dto, result);
    }
    
    [Fact(DisplayName = "Update method changing the name and description")]
    public async Task TestPcBuildServiceUpdateNameAndDescription()
    {
        var id = await SeedTestDataAsyncWithId();

        var dto = GetEditDto();

        dto.Id = id;
        dto.PcName = UpdatedPcName;
        dto.Description = UpdatedPcDesc;

        var result = await _service.Update(dto);

        Assert.NotNull(result);
        Assert.IsType<PcBuildEditDTO>(result);
        
        Assert.Equivalent(dto, result);
    }
    
    [Fact(DisplayName = "Update method changing the id of one component")]
    public async Task TestPcBuildServiceUpdateComponentIsEdited()
    {
        var id = await SeedTestDataAsyncWithId();

        var updated = _context.Add(new Component
        {
            CategoryId = AppDataIds.CategoryIds[CategoryNames.Case],
            ManufacturerId = _manufacturerId,
            DiscountId = _discountId,
            ComponentName = "Updated",
            Description = "Test",
            Price = Price,
            Stock = Stock   
        }).Entity;

        await _context.SaveChangesAsync();
        
        var dto = GetEditDto();

        dto.Id = id;
        dto.CaseId = updated.Id;

        var result = await _service.Update(dto);

        Assert.NotNull(result);
        Assert.IsType<PcBuildEditDTO>(result);
        
        Assert.Equivalent(dto, result);
    }
    
    [Fact(DisplayName = "Update method adding secondary storage")]
    public async Task TestPcBuildServiceUpdateSecondaryStorageIsAdded()
    {
        var id = await SeedTestDataAsyncWithId();

        var dto = GetEditDto();

        var secondaryStorage = await _context.PcComponents
            .Where(p => p.ComponentId == dto.SecondaryStorageId)
            .FirstOrDefaultAsync();
        
        _context.Remove(secondaryStorage!);
        await _context.SaveChangesAsync();

        dto.Id = id;
        dto.SecondaryStorageId = _componentIds[CategoryNames.HardDrive];
        
        var result = await _service.Update(dto);
        
        Assert.NotNull(result);
        Assert.IsType<PcBuildEditDTO>(result);
        
        Assert.Equivalent(dto, result);
        Assert.Equal(10, (await _context.PcComponents.ToListAsync()).Count);
    }
    
    [Fact(DisplayName = "Update method removing secondary storage")]
    public async Task TestPcBuildServiceUpdateSecondaryStorageIsRemoved()
    {
        var id = await SeedTestDataAsyncWithId();

        var dto = GetEditDto();

        dto.Id = id;
        dto.SecondaryStorageId = null;
        
        var result = await _service.Update(dto);
        
        Assert.NotNull(result);
        Assert.IsType<PcBuildEditDTO>(result);
        
        Assert.Equivalent(dto, result);
        Assert.Equal(9, (await _context.PcComponents.ToListAsync()).Count);
    }
    
    [Fact(DisplayName = "Update method changing a component to one with the wrong category")]
    public async Task TestPcBuildServiceUpdateComponentToInvalidCategory()
    {
        var id = await SeedTestDataAsyncWithId();

        var dto = GetEditDto();

        dto.Id = id;
        dto.CaseId = _componentIds[CategoryNames.Motherboard];

        await Assert.ThrowsAsync<ArgumentException>(() => _service.Update(dto));
    }
    
    [Fact(DisplayName = "Update method changing the name to empty")]
    public async Task TestPcBuildServiceUpdateInvalidName()
    {
        var id = await SeedTestDataAsyncWithId();

        var dto = GetEditDto();

        dto.Id = id;
        dto.PcName = "";

        await Assert.ThrowsAsync<ArgumentException>(() => _service.Update(dto));
        Assert.Equal(TestPcName, (await _context.PcBuilds.FindAsync(id))?.PcName);
    }

    [Fact(DisplayName = "Update method changing a component id to an invalid one")]
    public async Task TestPcBuildServiceUpdateInvalidComponentId()
    {
        var id = await SeedTestDataAsyncWithId();

        var dto = GetEditDto();

        dto.Id = id;
        dto.PcName = UpdatedPcName;
        dto.CaseId = Guid.Empty;

        await Assert.ThrowsAsync<ArgumentException>(() => _service.Update(dto));
        Assert.Equal(TestPcName, (await _context.PcBuilds.FindAsync(id))?.PcName);
    }
    
    [Fact(DisplayName = "Update method changing the discount id to an invalid one")]
    public async Task TestPcBuildServiceUpdateInvalidDiscountId()
    {
        var id = await SeedTestDataAsyncWithId();

        var dto = GetEditDto();

        dto.Id = id;
        dto.PcName = UpdatedPcName;
        dto.DiscountId = Guid.Empty;

        await Assert.ThrowsAsync<ArgumentException>(() => _service.Update(dto));
        Assert.Equal(TestPcName, (await _context.PcBuilds.FindAsync(id))?.PcName);
    }
    
    [Fact(DisplayName = "Update method changing the category id to an invalid one")]
    public async Task TestPcBuildServiceUpdateInvalidCategoryId()
    {
        var id = await SeedTestDataAsyncWithId();

        var dto = GetEditDto();

        dto.Id = id;
        dto.PcName = UpdatedPcName;
        dto.CategoryId = Guid.Empty;

        await Assert.ThrowsAsync<ArgumentException>(() => _service.Update(dto));
        Assert.Equal(TestPcName, (await _context.PcBuilds.FindAsync(id))?.PcName);
    }

    [Fact(DisplayName = "Add method")]
    public async Task TestPcBuildServiceAdd()
    {
        await SeedTestDataAsync();

        var result = await _service.Add(GetCreateDto());
        
        Assert.NotNull(result);
        Assert.IsType<PcBuildCreateDTO>(result);
        
        Assert.Equal(2, (await _context.PcBuilds.ToListAsync()).Count);
        Assert.Equal(20, (await _context.PcComponents.ToListAsync()).Count);
    }
    
    [Fact(DisplayName = "Add method with invalid name")]
    public async Task TestPcBuildServiceAddInvalidName()
    {
        await SeedTestDataAsync();

        var dto = GetCreateDto();

        dto.PcName = "";

        await Assert.ThrowsAsync<ArgumentException>(() => _service.Add(dto));
        Assert.Single(await _context.PcBuilds.ToListAsync());
    }

    [Fact(DisplayName = "Add method with an invalid component id")]
    public async Task TestPcBuildServiceAddInvalidComponentId()
    {
        await SeedTestDataAsync();

        var dto = GetCreateDto();

        dto.CaseId = Guid.Empty;

        await Assert.ThrowsAsync<ArgumentException>(() => _service.Add(dto));
        Assert.Single(await _context.PcBuilds.ToListAsync());
    }
    
    [Fact(DisplayName = "Add method with invalid discount id")]
    public async Task TestPcBuildServiceAddInvalidDiscountId()
    {
        await SeedTestDataAsync();

        var dto = GetCreateDto();

        dto.DiscountId = Guid.Empty;

        await Assert.ThrowsAsync<ArgumentException>(() => _service.Add(dto));
        Assert.Single(await _context.PcBuilds.ToListAsync());
    }
    
    [Fact(DisplayName = "Add method with invalid category id")]
    public async Task TestPcBuildServiceAddInvalidCategoryId()
    {
        await SeedTestDataAsync();

        var dto = GetCreateDto();

        dto.CategoryId = Guid.Empty;

        await Assert.ThrowsAsync<ArgumentException>(() => _service.Add(dto));
        Assert.Single(await _context.PcBuilds.ToListAsync());
    }
    
    [Fact(DisplayName = "RemoveAsync method")]
    public async Task TestPcBuildServiceRemoveAsync()
    {
        var id = await SeedTestDataAsyncWithId();

        var result = await _service.RemoveAsync(id);

        Assert.NotNull(result);
        Assert.IsType<PcBuildDTO>(result);
        Assert.Equal(TestPcName, result.PcName);

        var pcComponents = await _context.PcComponents.ToListAsync();
        
        Assert.Empty(pcComponents);
    }

    [Fact(DisplayName = "RemoveAsync method with invalid id")]
    public async Task TestPcBuildServiceRemoveAsyncInvalidInput()
    {
        await SeedTestDataAsync();

        var result = await _service.RemoveAsync(Guid.Empty);

        Assert.Null(result);

        var pcComponents = await _context.PcComponents.ToListAsync();
        
        Assert.Equal(10, pcComponents.Count);   
    }

    private PcBuildCreateDTO GetCreateDto()
    {
        return new PcBuildCreateDTO
        {
            PcName = TestPcName,
            Description = TestPcDesc,
            Stock = Stock,
            DiscountId = _discountId,
            CategoryId = AppDataIds.CategoryIds[CategoryNames.TemplatePc],
            CaseId = _componentIds[CategoryNames.Case],
            MotherboardId = _componentIds[CategoryNames.Motherboard],
            ProcessorId = _componentIds[CategoryNames.Processor],
            CpuCoolerId = _componentIds[CategoryNames.CpuCooler],
            MemoryId = _componentIds[CategoryNames.Memory],
            GraphicsCardId = _componentIds[CategoryNames.GraphicsCard],
            PrimaryStorageId = _componentIds[CategoryNames.SolidStateDrive],
            SecondaryStorageId = _componentIds[CategoryNames.HardDrive],
            PowerSupplyId = _componentIds[CategoryNames.PowerSupply],
            OperatingSystemId = _componentIds[CategoryNames.OperatingSystem]
        };
    }

    private PcBuildEditDTO GetEditDto()
    {
        return new PcBuildEditDTO
        {
            Id = Guid.Empty,
            PcName = TestPcName,
            Description = TestPcDesc,
            Stock = Stock,
            DiscountId = _discountId,
            CategoryId = AppDataIds.CategoryIds[CategoryNames.TemplatePc],
            CaseId = _componentIds[CategoryNames.Case],
            MotherboardId = _componentIds[CategoryNames.Motherboard],
            ProcessorId = _componentIds[CategoryNames.Processor],
            CpuCoolerId = _componentIds[CategoryNames.CpuCooler],
            MemoryId = _componentIds[CategoryNames.Memory],
            GraphicsCardId = _componentIds[CategoryNames.GraphicsCard],
            PrimaryStorageId = _componentIds[CategoryNames.SolidStateDrive],
            SecondaryStorageId = _componentIds[CategoryNames.HardDrive],
            PowerSupplyId = _componentIds[CategoryNames.PowerSupply],
            OperatingSystemId = _componentIds[CategoryNames.OperatingSystem]
        };
    }
    
    private async Task SeedTestDataAsync(int count = 1, string pcCategory = CategoryNames.TemplatePc)
    {
        if (count == 0) return;
        
        SeedCategories();
        SeedDiscount();
        SeedManufacturer();
    
        SeedComponents(_manufacturerId, _discountId);
        
        for (var i = 0; i < count; i++)
        {
            var pcBuild = _context.PcBuilds.Add(new PcBuild
            {
                CategoryId = AppDataIds.CategoryIds[pcCategory],
                DiscountId = _discountId,
                PcName = TestPcName,
                Description = TestPcDesc,
                Stock = Stock,
            }).Entity;
            
           await AddComponentsToPc(pcBuild.Id);
        }
        await _context.SaveChangesAsync();
    }

    private async Task<Guid> SeedTestDataAsyncWithId()
    {
        SeedCategories();
        SeedDiscount();
        SeedManufacturer();
    
        SeedComponents(_manufacturerId, _discountId);

        var pcBuild = _context.PcBuilds.Add(new PcBuild
        {
            CategoryId = AppDataIds.CategoryIds[CategoryNames.TemplatePc],
            DiscountId = _discountId,
            PcName = TestPcName,
            Description = TestPcDesc,
            Stock = Stock,
        }).Entity;
        
        await AddComponentsToPc(pcBuild.Id);

        await _context.SaveChangesAsync();
        _context.Entry(pcBuild).State = EntityState.Detached;
        
        return pcBuild.Id;
    }

    private async Task AddComponentsToPc(Guid pcBuildId)
    {
        foreach (var id in _componentIds.Values)
        {
            var pcComponent = _context.PcComponents.Add(new PcComponent
            {
                ComponentId = id,
                PcBuildId = pcBuildId,
                Qty = 1
            }).Entity;

            await _context.SaveChangesAsync();
            _context.Entry(pcComponent).State = EntityState.Detached;
        }
    }

    private void SeedDiscount()
    {
        _context.Discounts.Add(new Discount
        {
            Id = _discountId,
            DiscountName = "Test Discount",
            DiscountPercentage = 0
        });   
    }

    private void SeedManufacturer()
    {
        _context.Manufacturers.Add(new Manufacturer
        {
            Id = _manufacturerId,
            ManufacturerName = "Test Manufacturer"
        });
    }

    private void SeedComponents(Guid manufacturerId, Guid discountId)
    {
        foreach (var category in _componentCategories)
        {
            var component = _context.Components.Add(new Component
            {
                CategoryId = AppDataIds.CategoryIds[category],
                ManufacturerId = manufacturerId,
                DiscountId = discountId,
                ComponentName = "Test " + category,
                Description = "Test",
                Price = Price,
                Stock = Stock
            }).Entity;

            _componentIds.Add(category, component.Id);
        }
    }

    private void SeedCategories()
    {
        foreach (var category in _componentCategories)
        {
            AddCategory(category);
        }
        
        foreach (var category in _pcCategories)
        {
            AddCategory(category);
        }
    }

    private void AddCategory(string category)
    {
        _context.Categories.Add(new Category
        {
            Id = AppDataIds.CategoryIds[category],
            CategoryName = category
        });      
    }
}