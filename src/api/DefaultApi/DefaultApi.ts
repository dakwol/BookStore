import BaseModelAPI from "../BaseModelAPI";
import apiConfig from "../apiConfig";
import axiosClient from "../axiosClient";

class DefaultApiRequest extends BaseModelAPI {
    constructor(apiUrl:string) {
        super(apiUrl);
    }
}

export default DefaultApiRequest;
