import { IToken } from "../../../models/IToken";
import { IUser } from "../../../models/IUser";

export interface AuthState {
    isAuth: boolean;
    user: IUser;
    token: IToken;
    isLoading: boolean;
    error: string;
    data: string;
}

export enum AuthActionEnum {
    SET_AUTH = 'SET_AUTH',
    SET_ERROR = 'SET_ERROR',
    SET_USER = 'SET_USER',
    SET_IS_LOADING = 'SET_IS_LOADING',
    SET_TOKEN = 'SET_TOKEN',
    SET_DATA = 'SET_DATA',
    CLEAR_DATA = 'CLEAR_DATA',
}

export interface SetAuthAction {
    type: AuthActionEnum.SET_AUTH;
    payload: boolean;
}
export interface SetErrorAction {
    type: AuthActionEnum.SET_ERROR;
    payload: string;
}
export interface SetDataAction {
    type: AuthActionEnum.SET_DATA;
    payload: string;
}

export interface ClearDataAction {
    type: AuthActionEnum.CLEAR_DATA;
}
export interface SetUserAction {
    type: AuthActionEnum.SET_USER;
    payload: IUser;
}
export interface SetIsLoadingAction {
    type: AuthActionEnum.SET_IS_LOADING;
    payload: boolean;
}
export interface SetTokenAction {
    type: AuthActionEnum.SET_TOKEN;
    payload: IToken;
}

export type AuthAction = 
    SetAuthAction |
    SetErrorAction |
    SetUserAction |
    SetIsLoadingAction | 
    SetDataAction | 
    ClearDataAction | 
    SetTokenAction