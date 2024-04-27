import React, { FC, useEffect, useState } from "react";
import FormInput from "../FormInput/FormInput";
import Buttons from "../Buttons/Buttons";
import { ISendModeration } from "../../models/ISendModeration";
import { fieldToArray } from "../UI/functions/functions";
import { ISendLogin } from "../../models/ISendLogin";
import { Link, useNavigate } from "react-router-dom";
import icons from "../../assets/icons/icons";
import "./styles.scss";
import { useDispatch } from "react-redux";
import { DataPressActionCreators } from "../../store/reducers/dataPressItem/action-creator";
import { useTypeSelector } from "../../hooks/useTypedSelector";
import { AuthActionCreators } from "../../store/reducers/auth/action-creator";
import ErrorMessage from "../UI/ErrorMassage/ErrorMassage";
// import EmployersApiRequest from "../../api/Employers/Employers";
import { RouteNames } from "../../routes";

interface iLoginForm {
  title: string;
  subTitle: string;
  dataOption: ISendModeration | ISendLogin | undefined;
}

const LoginComponents: FC<iLoginForm> = ({ title, subTitle, dataOption }) => {
  // const employersApi = new EmployersApiRequest();

  const dispatch = useDispatch();
  const dataPress = useTypeSelector(
    (state: any) => state.dataPressReducer.dataPress
  );

  const navigate = useNavigate();

  const [isRegistration, setIsRegistration] = useState<boolean>(false);

  const handleChange = (fieldName: string, fieldValue: string | boolean) => {
    dispatch(DataPressActionCreators.setDataPress(fieldName, fieldValue));
  };

  const { isAuth, error, isLoading } = useTypeSelector(
    (state) => state.authReducer
  );

  const submit = () => {
    console.log(dataPress);

    !isRegistration
      ? dispatch(
          //@ts-ignore
          AuthActionCreators.login(dataPress.email, dataPress.password)
        )
      : dispatch(
          //@ts-ignore
          AuthActionCreators.registration(
            dataPress.name,
            dataPress.email,
            dataPress.password
          )
        );
  };

  useEffect(() => {
    if (isAuth) {
      navigate(RouteNames.HOMEPAGE);
    }
  }, [isAuth]);

  return (
    <div className="formContainer">
      {error != "" && (
        <ErrorMessage
          type={"error"}
          message={error}
          onClick={() => {
            submit();
          }}
          onClose={() => {
            dispatch(AuthActionCreators.setErr(""));
          }}
        />
      )}
      <div className="formContainerField">
        <div className="titleFormContainer">
          {subTitle && <h3 className="formSubTitle">{subTitle}</h3>}
          {title && <h2 className="formTitle">{title}</h2>}
        </div>

        <div className="inputContainerForm">
          {dataOption &&
            fieldToArray(dataOption).map((item) => {
              if (!isRegistration && item.key === "name") {
                return;
              }
              return (
                <FormInput
                  style={""}
                  value={dataPress[item.key]}
                  onChange={(e) => {
                    handleChange(item.key, e);
                  }}
                  subInput={item.value.label}
                  required={item.value.required}
                  type={item.value.type}
                  mask={item.value.style && item.value.style.mask}
                  placeholder={item.value.style && item.value.style.placeholder}
                  error={false}
                  keyData={item.key}
                  friedlyInput={true}
                />
              );
            })}
        </div>
        <div className="buttonLoginFormContainer">
          <Buttons
            text={
              isLoading ? "Загрузка" : isRegistration ? "Регистрация" : "Вход"
            }
            onClick={() => submit()}
          />
          <Buttons
            text={
              isLoading ? "Загрузка" : !isRegistration ? "Регистрация" : "Вход"
            }
            onClick={() => setIsRegistration(!isRegistration)}
          />
        </div>
      </div>
    </div>
  );
};

export default LoginComponents;
