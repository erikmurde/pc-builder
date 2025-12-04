export interface IBaseItemProps<TEntity> {
    key: number,
    index: number,
    entity: TEntity,

    onDelete?: (id: string) => void
}