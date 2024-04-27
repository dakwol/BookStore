
import BaseModelAPI from "../BaseModelAPI";
import apiConfig from "../apiConfig";
import axiosClient from "../axiosClient";
import { API_GENRE_MODEL } from "./const";

class GenreApiRequest extends BaseModelAPI {
    constructor() {
        super(API_GENRE_MODEL.url);
    }
}

export default GenreApiRequest;
