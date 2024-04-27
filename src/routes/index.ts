import React from "react";
import Login from "../pages/Login/Login";
import HomePage from "../pages/HomePage/HomePage";
import AdminPanelPage from "../pages/AdminPanelPage/AdminPanelPage";
import BookPage from "../pages/BookPage/BookPage";
import CategoryPage from "../pages/CategoryPage/CategoryPage";
const isAuthenticated = localStorage.getItem("account");
const isAuthenticatedApplicant = localStorage.getItem("applicant");

export interface IRoute {
    path: string;
    element : React.ComponentType;
    exact?: boolean;
    params?: { [key: string]: string | number };
}

export enum RouteNames {
    LOGIN = '/login',
    ADMIN = '/admin-panel',
    HOMEPAGE = '/',
    BOOK = '/book',
    CATEGORY = '/category',
}

export const publicRoutes: IRoute[] = [
  {path: RouteNames.HOMEPAGE, exact: true, element: HomePage},
  {path: RouteNames.LOGIN, exact: false, element: Login},
  {path:`${RouteNames.BOOK}/:id`, exact: false, element: BookPage, params: { params: ':id' }},
  {path:`${RouteNames.CATEGORY}/:category`, exact: false, element: CategoryPage, params: { params: ':category' }},
]

export const privateRoutes: IRoute[] = [
    {path: RouteNames.HOMEPAGE, exact: true, element: HomePage},
    {path: RouteNames.LOGIN, exact: false, element: Login},
    {path: RouteNames.ADMIN, exact: false, element: AdminPanelPage},
    {path:`${RouteNames.BOOK}/:id`, exact: false, element: BookPage, params: { params: ':id' }},
    {path:`${RouteNames.CATEGORY}/:category`, exact: false, element: CategoryPage, params: { params: ':category' }},
]