export interface CardData {
    id: string | number;
    title: string;
    year: string;
    authorId: string;
    author?: {id: number | string, name: string};
    genreId: string;
    genre?: {id: number | string, name: string};
    publisher?: {id: number | string, name: string};
    publisherId?: string;
    rating: string;
    description?:string;
    imagePreview?:string
  }