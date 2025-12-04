export interface IPcBuildComponentsDTO {
    caseId: string,
    motherboardId: string,
    processorId: string,
    cpuCoolerId: string,
    memoryId: string,
    graphicsCardId: string,
    primaryStorageId: string,
    secondaryStorageId?: string,
    powerSupplyId: string,
    operatingSystemId: string
}