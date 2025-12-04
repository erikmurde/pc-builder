using System.ComponentModel.DataAnnotations;

namespace DAL.EF.Base;

public class DecimalRange : RegularExpressionAttribute
{
    public DecimalRange(int precision, int scale) : 
        base($@"^\d{{1,{precision - scale}}}(\D\d{{1,{scale}}})?$")
    {
    }
}