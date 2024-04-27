import { AppDispatch } from "../../index";
import { IUser, IUserReg } from "../../../models/IUser";
import { AuthActionEnum, ClearDataAction, SetAuthAction, SetDataAction, SetErrorAction, SetIsLoadingAction, SetTokenAction, SetUserAction } from "./types";
import { IToken } from "../../../models/IToken";
import jwt from 'jwt-decode';
import UserApiRequest from "../../../api/User/Users";

export const AuthActionCreators = {
    setUser: (user: IUser): SetUserAction => ({ type: AuthActionEnum.SET_USER, payload: user }),
    setToken: (token: IToken): SetTokenAction => ({ type: AuthActionEnum.SET_TOKEN, payload: token }),
    setIsAuth: (auth: boolean): SetAuthAction => ({ type: AuthActionEnum.SET_AUTH, payload: auth }),
    setErr: (payload: string): SetErrorAction => ({ type: AuthActionEnum.SET_ERROR, payload }),
    setData: (payload: string): SetDataAction => ({ type: AuthActionEnum.SET_DATA, payload }),
    clearData: (): ClearDataAction => ({ type: AuthActionEnum.CLEAR_DATA }),


    setIsLoading: (payload: boolean): SetIsLoadingAction => ({ type: AuthActionEnum.SET_IS_LOADING, payload }),
    login: (email: string, password: string) => async (dispatch: AppDispatch) => {
        dispatch(AuthActionCreators.setIsLoading(true));
        const mockUser = { email, password };
        const userData = new UserApiRequest();
        console.log('mockUser',mockUser);
        
        setTimeout(()=>{
            if (mockUser.email === undefined || mockUser.email.length === 0 || mockUser.password === undefined || mockUser.password.length === 0) {
                dispatch(AuthActionCreators.setErr('Некорректный логин или пароль'));
                dispatch(AuthActionCreators.setIsLoading(false));
                return;
            }
            try {
                userData.login(mockUser).then((resp)=>{
                    if (resp.success) {
                        //@ts-ignore
                        const tokens = resp.data as IToken;
                        dispatch(AuthActionCreators.setToken(tokens));
                        localStorage.setItem('access', tokens.accessToken || '')
                        localStorage.setItem('refresh', tokens.refreshToken || '')
                       //@ts-ignore
                        const decodeJwt = jwt(tokens.accessToken) || '';
                        
                        //@ts-ignore
                        if (decodeJwt && decodeJwt.Id) {
                            //@ts-ignore
                            userData.getById({id: decodeJwt.Id + '/'}).then((resp)=>{
                                
                                if(resp.success){
                                    localStorage.setItem('auth', "true");
                                    localStorage.setItem('email', mockUser.email);
                                    if (resp.data) {
                                        //@ts-ignore
                                        const dataUser: { id?: string, email?: string, name?: string, role?:string } = resp.data;

                                        const user = {
                                            id: dataUser.id,
                                            email: dataUser.email,
                                            name: dataUser.name,
                                            role: dataUser.role,
                                        };
                                    
                                        localStorage.setItem('account', JSON.stringify(user));
                                        dispatch(AuthActionCreators.setIsAuth(true));
                                    }
                                   
                                    
                                    //@ts-ignore
                                    dispatch(AuthActionCreators.setUser({username: resp.data.username, password: mockUser.password, firstname: resp.data.first_name, lastname: resp.data.last_name, patronymic: resp.data.patronymic}));
                                } else {
                                    dispatch(AuthActionCreators.setErr('Ошибка получения пользователя'));
                                }
                            })
                          
                        }
                      
                    } else {
                        dispatch(AuthActionCreators.setErr('Произошла ошибка авторизации'));
                    }
                });
               
            } catch (e) {
                dispatch(AuthActionCreators.setErr('Произошла ошибка при авторизации'));
            }
            dispatch(AuthActionCreators.setIsLoading(false));
        }, 2000)
      
    },
    registration: (name: string,email:string, password: string) => async (dispatch: AppDispatch) => {
        dispatch(AuthActionCreators.setIsLoading(true));
        const mockUser = {name, email, password };
        const userData = new UserApiRequest();
        setTimeout(()=>{
            if (mockUser.email.length === 0 || mockUser.password.length === 0) {
                dispatch(AuthActionCreators.setErr('Некорректный логин или пароль'));
                dispatch(AuthActionCreators.setIsLoading(false));
                return;
            }
            try {
                userData.registration(mockUser ).then((resp)=>{
                    if (resp.success) {
                        //@ts-ignore
                        const tokens = resp.data as IToken;
                        dispatch(AuthActionCreators.setToken(tokens));
                        localStorage.setItem('access', tokens.accessToken || '')
                        localStorage.setItem('refresh', tokens.refreshToken || '')
                        //@ts-ignore
                        const decodeJwt = jwt(tokens.accessToken) || '';
                        
                        //@ts-ignore
                        if (decodeJwt && decodeJwt.Id) {
                                  //@ts-ignore
                            userData.getById({id: decodeJwt.Id + '/'}).then((resp)=>{
                                
                                if(resp.success){
                                    localStorage.setItem('auth', "true");
                                    localStorage.setItem('email', mockUser.email);
                                    if (resp.data) {
                                        //@ts-ignore
                                        const dataUser: { id?: string, email?: string, name?: string, role?:string } = resp.data;

                                        const user = {
                                            id: dataUser.id,
                                            email: dataUser.email,
                                            name: dataUser.name,
                                            role: dataUser.role,
                                        };
                                    
                                        localStorage.setItem('account', JSON.stringify(user));
                                        dispatch(AuthActionCreators.setIsAuth(true));
                                    }
                                   
                                    
                                    //@ts-ignore
                                    dispatch(AuthActionCreators.setUser({username: resp.data.username, password: mockUser.password, firstname: resp.data.first_name, lastname: resp.data.last_name, patronymic: resp.data.patronymic}));
                                } else {
                                    dispatch(AuthActionCreators.setErr('Ошибка получения пользователя'));
                                }
                            })
                          
                        }
                      
                    } else {
                        dispatch(AuthActionCreators.setErr('Произошла ошибка авторизации'));
                    }
                });
               
            } catch (e) {
                dispatch(AuthActionCreators.setErr('Произошла ошибка при авторизации'));
            }
            dispatch(AuthActionCreators.setIsLoading(false));
        }, 2000)
      
    },
    logout: () => async (dispatch: AppDispatch) => {
        dispatch(AuthActionCreators.setIsLoading(true));
        localStorage.removeItem('auth');
        localStorage.removeItem('username');
        localStorage.removeItem('applicant');
        localStorage.removeItem('account');
        localStorage.removeItem('access');
        localStorage.removeItem('accountEmployers');
        localStorage.removeItem('refresh');
        localStorage.removeItem('userVK');
        dispatch(AuthActionCreators.setIsAuth(false));
        dispatch(AuthActionCreators.setUser({} as IUser));
        dispatch(AuthActionCreators.setIsLoading(false));
    }
}
