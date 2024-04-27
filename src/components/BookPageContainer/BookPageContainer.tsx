import React, { FC } from "react";
import Buttons from "../Buttons/Buttons";
import icons from "../../assets/icons/icons";
import { useNavigate } from "react-router-dom";
import { CardData } from "../../models/ICardData";
import "./styles.scss";

interface ItemBook {
  itemBook: CardData;
}

const BookPageContainer: FC<ItemBook> = ({ itemBook }) => {
  const navigation = useNavigate();
  return (
    <div className="container">
      <Buttons
        ico={icons.ArrowBack}
        text={"Назад"}
        onClick={() => navigation(-1)}
        className="backButton"
      />

      <div className="infoBook">
        <img src={itemBook.imagePreview} className="imageBook"></img>

        <div className="containerInfoBook">
          <h1 className="nameInfoBook">{`${itemBook.title} №${itemBook.id}`}</h1>
          <h3 className="authorInfoBook">{itemBook.author?.name}</h3>
          <p className="textInfoBook">{`Рейтинг: ${itemBook.rating}`}</p>
          <p className="textInfoBook">{`Год издания: ${itemBook.year}`}</p>
          <p className="textInfoBook">{`Жанр: ${itemBook.genre?.name}`}</p>
          <p className="textInfoBook">{`Издатель: ${itemBook.publisher?.name}`}</p>
          <h1 className="subtitleInfoBook">Описание</h1>
          <p className="textInfoBook">{itemBook.description}</p>
        </div>
      </div>
    </div>
  );
};

export default BookPageContainer;
