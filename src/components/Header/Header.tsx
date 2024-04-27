import React, { FC } from "react";
import InputSearch from "../InputSearch/InputSearch";
import icons from "../../assets/icons/icons";
import { useLocation, useNavigate } from "react-router-dom";
import "./styles.scss";
import Buttons from "../Buttons/Buttons";
import { RouteNames } from "../../routes";
import { Link } from "react-router-dom";
import { AuthActionCreators } from "../../store/reducers/auth/action-creator";
import { useDispatch } from "react-redux";

const Header: FC = () => {
  const navigation = useNavigate();
  const dispatch = useDispatch();
  const location = useLocation();
  const userData = JSON.parse(localStorage.getItem("account") || "{}");

  const dataNav = [
    ...(userData.role === "Admin"
      ? [
          {
            id: 1,
            ico: icons.user,
            onClick: () => navigation(RouteNames.ADMIN),
          },
        ]
      : []),
    {
      id: 2,
      ico: icons.HEART,
      onClick: () => logOut(),
    },
  ];

  const logOut = () => {
    //@ts-ignore
    dispatch(AuthActionCreators.logout());
    navigation(RouteNames.LOGIN);
  };

  return (
    <header className="header">
      <div className="container">
        <div className="headerContainer">
          <Link to={RouteNames.HOMEPAGE}>
            <img src={icons.Logo} alt="Logo"></img>
          </Link>

          <nav className="navContainer">
            {location.pathname === RouteNames.ADMIN ? (
              <Buttons
                text={"На главную"}
                onClick={() => navigation(RouteNames.HOMEPAGE)}
              />
            ) : (
              dataNav.map((item) => (
                <Buttons
                  key={item.id}
                  ico={item.ico}
                  text={""}
                  onClick={item.onClick}
                  className="buttonNav"
                />
              ))
            )}
          </nav>
        </div>
      </div>
    </header>
  );
};

export default Header;
