import React, { FC, Fragment, useEffect, useState } from "react";
import Buttons from "../Buttons/Buttons";
import "./styles.scss";
import { CardData } from "../../models/ICardData";
import icons from "../../assets/icons/icons";
import Modal from "../Modal/Modal";
import { fieldToArray } from "../UI/functions/functions";
import { CreateBooksInput } from "../../models/ICreateBooks";
import FormInput from "../FormInput/FormInput";
import { useDispatch } from "react-redux";
import { DataPressActionCreators } from "../../store/reducers/dataPressItem/action-creator";
import { useTypeSelector } from "../../hooks/useTypedSelector";
import BookApiRequest from "../../api/Book/Book";
import DefaultApiRequest from "../../api/DefaultApi/DefaultApi";
import ErrorMessage from "../UI/ErrorMassage/ErrorMassage";

const AdminPanelContainer: FC = () => {
  const [isOpen, setIsOpen] = useState(false);
  const [isEditMode, setIsEditMode] = useState(false);
  const [isError, setIsError] = useState({ message: "" });
  const [selectedBook, setSelectedBook] = useState<CardData | null>(null);
  const [isUpdate, setIsUpdate] = useState(false);
  const [dataBook, setDataBook] = useState<CardData[]>([]);
  const dispatch = useDispatch();
  const booksApi = new BookApiRequest();
  const dataPress = useTypeSelector(
    (state: any) => state.dataPressReducer.dataPress
  );

  const handleChange = (fieldName: string, fieldValue: string | boolean) => {
    dispatch(DataPressActionCreators.setDataPress(fieldName, fieldValue));
  };

  useEffect(() => {
    booksApi.list().then((resp) => {
      if (resp.success) {
        setDataBook(resp.data as CardData[]);
      }
    });
  }, [isUpdate]);

  const createBook = () => {
    const missingFields = fieldToArray(CreateBooksInput)
      .filter((item) => item.value.required)
      .filter((item) => !dataPress[item.key]);

    if (missingFields.length > 0) {
      setIsError({
        message: `Необходимо заполнить следующие поля: ${missingFields
          .map((item) => item.value.label)
          .join(", ")}`,
      });
      return;
    }

    booksApi.create({ body: dataPress }).then((resp) => {
      if (resp.success) {
        setIsOpen(false);
        setIsUpdate(!isUpdate);
      }
    });
  };

  const updateBook = () => {
    const missingFields = fieldToArray(CreateBooksInput)
      .filter((item) => item.value.required)
      .filter((item) => !dataPress[item.key]);

    if (missingFields.length > 0) {
      setIsError({
        message: `Необходимо заполнить следующие поля: ${missingFields
          .map((item) => item.value.label)
          .join(", ")}`,
      });
      return;
    }
    booksApi
      .update({ id: selectedBook?.id as string, body: dataPress })
      .then((resp) => {
        if (resp.success) {
          setIsOpen(false);
          setIsUpdate(!isUpdate);
        }
      });
  };
  const deleteBook = (id: string) => {
    booksApi.delete({ id: id }).then((resp) => {
      if (resp.success) {
        setIsOpen(false);
        setIsUpdate(!isUpdate);
      }
    });
  };

  const openModal = (
    editMode: boolean = false,
    book: CardData | null = null
  ) => {
    setIsOpen(true);
    setIsEditMode(editMode);
    setSelectedBook(book);
  };

  const handleSave = () => {
    if (isEditMode && selectedBook) {
      updateBook();
    } else {
      createBook();
    }
  };

  useEffect(() => {
    dispatch(DataPressActionCreators.clearDataPress());
  }, [isOpen]);

  useEffect(() => {
    if (isEditMode) {
      fieldToArray(selectedBook as CardData).map((item) => {
        dispatch(DataPressActionCreators.setDataPress(item.key, item.value));
      });
    }
  }, [isEditMode, selectedBook]);

  console.log("ddd", dataPress);

  const [options, setOptions] = useState({});

  useEffect(() => {
    const fetchOptions = async () => {
      const options = {};
      await Promise.all(
        fieldToArray(CreateBooksInput).map(async (item) => {
          const choices = await getChoices(item.value.choicesUrl);
          //@ts-ignore
          options[item.key] = choices;
        })
      );
      setOptions(options);
    };

    fetchOptions();
  }, []);

  const getChoices = async (choicesUrl: string) => {
    if (choicesUrl === "" || choicesUrl === undefined) {
      return [];
    } else {
      const defaultApi = new DefaultApiRequest(choicesUrl);

      try {
        const resp = await defaultApi.list();

        if (resp.success) {
          const dataChoices = resp.data
            ? resp.data.map((item: any) => ({
                id: item.id,
                value: item.id,
                display_name: item.name,
              }))
            : [];
          return dataChoices;
        }
      } catch (error) {
        console.error("Error fetching choices:", error);
        return [];
      }
    }
  };

  const modalContent = (
    <div className="containerModalCreate">
      <h1 className="modalTitle">
        {isEditMode ? "Редактирование книги" : "Добавление книги"}
      </h1>
      {fieldToArray(CreateBooksInput).map((item) => (
        <FormInput
          style=""
          value={dataPress[item.key]}
          onChange={(value) => handleChange(item.key, value)}
          subInput={item.value.label}
          required={item.value.required}
          error=""
          options={
            //@ts-ignore
            options[item.key] || []
          }
          type={item.value.type}
          keyData=""
          friedlyInput
          textArea={item.key === "description"}
          key={item.key}
        />
      ))}
      <Buttons
        text={isEditMode ? "Сохранить" : "Создать"}
        onClick={handleSave}
      />
    </div>
  );

  return (
    <Fragment>
      {isError.message !== "" && (
        <ErrorMessage
          type={"error"}
          message={isError.message}
          onClose={() => setIsError({ message: "" })}
        />
      )}
      <Modal
        content={isOpen && modalContent}
        isOpen={isOpen}
        onClose={() => setIsOpen(false)}
      />
      <section>
        <div className="container">
          <div className="adminContainer">
            <h1 className="titleAdmin">Управление библиотекой</h1>
            <Buttons text="Добавить книгу" onClick={() => openModal(false)} />
          </div>

          <div>
            {dataBook.map((item) => {
              return (
                <div className="lineCardBook">
                  <p className="lineID">{item.id}</p>
                  <img src={item.imagePreview} className="lineImg"></img>
                  <p className="lineName">{item.title}</p>
                  {item.author && (
                    <p className="lineAuthor">{item.author.name}</p>
                  )}
                  <p className="linePrice">{item.rating}</p>
                  <p className="linePrice">{item.year}</p>
                  {item.genre && <p className="linePrice">{item.genre.name}</p>}
                  {item.publisher && (
                    <p className="linePrice">{item.publisher.name}</p>
                  )}

                  <div className="buttonAdminContainer">
                    <Buttons
                      text=""
                      onClick={() => openModal(true, item)}
                      ico={icons.pencilLine}
                      className="adminButton"
                    />
                    <Buttons
                      text=""
                      onClick={() => {
                        deleteBook(item.id as string);
                      }}
                      ico={icons.xClosed}
                      className="adminButton"
                    />
                  </div>
                </div>
              );
            })}
          </div>
        </div>
      </section>
    </Fragment>
  );
};

export default AdminPanelContainer;
