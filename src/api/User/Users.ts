import BaseModelAPI from "../BaseModelAPI";
import apiConfig from "../apiConfig";
import axiosClient from "../axiosClient";
import { API_USER_MODEL } from "./const";

class UserApiRequest extends BaseModelAPI {
    constructor() {
        super(API_USER_MODEL.url);
    }

    async login<T>(body?: any) {
        return this.makeRequest<T>(axiosClient.post, {method: API_USER_MODEL.methods.login.url, body: body});
    }
    async registration<T>(body?: any) {
        return this.makeRequest<T>(axiosClient.post, {method: API_USER_MODEL.methods.register.url, body: body});
    }
}

export default UserApiRequest;
