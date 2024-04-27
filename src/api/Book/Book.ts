
import BaseModelAPI from "../BaseModelAPI";
import apiConfig from "../apiConfig";
import axiosClient from "../axiosClient";
import { API_BOOK_MODEL } from "./const";

class BookApiRequest extends BaseModelAPI {
    constructor() {
        super(API_BOOK_MODEL.url);
    }
}

export default BookApiRequest;
