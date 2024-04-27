import React, { FC, Fragment, useEffect, useState } from "react";
import "./styles.scss";
import { useNavigate } from "react-router-dom";
import icons from "../../assets/icons/icons";
import LoginComponents from "../../components/LoginComponents/LoginComponents";

const Login: FC = () => {
  const navigation = useNavigate();

  const dataInput = {
    name: {
      label: "Имя",
      read_only: true,
      required: true,
      type: "string",
    },
    email: {
      label: "E-mail",
      read_only: true,
      required: true,
      type: "email",
    },

    password: {
      label: "Пароль",
      read_only: true,
      required: true,
      type: "string",
    },
  };

  return (
    <section className="sectionLogin">
      <img src={icons.Bg} className="bgLogin"></img>

      <div className="loginContainer">
        <img src={icons.Logo} className="logoLogin"></img>
        <LoginComponents
          title={"Вход в аккаунт"}
          subTitle={"Добро пожаловать!"}
          dataOption={dataInput}
        />
      </div>
    </section>
  );
};

export default Login;
