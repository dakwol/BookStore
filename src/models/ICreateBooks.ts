import { API_AUTHOR_MODEL, API_COUNTRIES_MODEL, API_PUBLISHED_MODEL } from "../api/DefaultApi/const";
import { API_GENRE_MODEL } from "../api/Genre/const";
import { IOptionInput } from "./IOptionInput";

export interface ICreateBooks {
    title: IOptionInput;
    year: IOptionInput;
    description: IOptionInput
    authorId: IOptionInput
    imagePreview: IOptionInput
    genreId: IOptionInput
    publisherId: IOptionInput
    rating: IOptionInput
    countriesId: IOptionInput
}

export const CreateBooksInput: ICreateBooks = {
    title: {
        label: "Название",
        read_only: true,
        required: true,
        type: "string",
    },
    year: {
        label: "Год",
        read_only: true,
        required: true,
        type: "number",
    },
    imagePreview: {
        label: "Ссылка на изображение",
        read_only: true,
        required: true,
        type: "url",
    },
    description: {
        label: "Описание",
        read_only: true,
        required: true,
        type: "string",
    },
    genreId: {
        label: "Жанр",
        read_only: true,
        required: true,
        type: "string",
        choicesUrl: API_GENRE_MODEL.url
    },
    authorId: {
        label: "Автор",
        read_only: true,
        required: true,
        type: "string",
        choicesUrl: API_AUTHOR_MODEL.url
    },
    countriesId: {
        label: "Страна",
        read_only: true,
        required: true,
        type: "string",
        choicesUrl: API_COUNTRIES_MODEL.url
    },
    publisherId: {
        label: "Издатель",
        read_only: true,
        required: true,
        type: "string",
        choicesUrl: API_PUBLISHED_MODEL.url
    },
    rating: {
        label: "Рейтинг",
        read_only: true,
        required: true,
        type: "string",
    },
};
