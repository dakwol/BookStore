import React, { FC, useEffect, useState } from "react";
import Header from "../../components/Header/Header";
import BookPageContainer from "../../components/BookPageContainer/BookPageContainer";
import icons from "../../assets/icons/icons";
import { CardData } from "../../models/ICardData";
import BookApiRequest from "../../api/Book/Book";
import { useParams } from "react-router-dom";

const BookPage: FC = () => {
  const { id } = useParams();
  const [dataBook, setDataBook] = useState<CardData>();
  const bookApi = new BookApiRequest();

  useEffect(() => {
    bookApi.getById({ id: id }).then((resp) => {
      if (resp.success) {
        resp.data && setDataBook(resp.data as CardData);
      }
    });
  }, []);

  return <div>{dataBook && <BookPageContainer itemBook={dataBook} />}</div>;
};

export default BookPage;
