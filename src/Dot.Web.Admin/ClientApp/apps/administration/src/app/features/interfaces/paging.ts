export interface PagingParam {
  pageNumber: number;
  pageSize: number;
}

export interface PagingResult<T> {
  items: T[];
  totalItems: number;
}
