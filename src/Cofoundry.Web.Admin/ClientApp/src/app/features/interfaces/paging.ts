export interface PagingParam {
    limit: number;
    offset: number;
}

export interface PagingResult<T> {
    collection: T[];
    total: number;
}