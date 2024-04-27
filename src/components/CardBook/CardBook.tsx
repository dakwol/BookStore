import React, { FC } from "react";
import { CardData } from "../../models/ICardData";
import "./styles.scss";
import { useNavigate } from "react-router-dom";
import { RouteNames } from "../../routes";

interface ItemCard {
  itemCard: CardData;
  direction?: boolean;
}

const CardBook: FC<ItemCard> = ({ itemCard, direction }) => {
  const navigation = useNavigate();
  return (
    <div
      key={itemCard.id}
      className={`containerCard ${direction && "column"}`}
      onClick={() => navigation(`${RouteNames.BOOK}/${itemCard.id}`)}
    >
      <img
        src={itemCard.imagePreview}
        className={`imageCard ${direction && "column"}`}
      ></img>
      <div className={`containerCardInfo ${direction && "column"}`}>
        <div>
          <h2 className="cardName">{itemCard.title}</h2>
          <h3 className="cardAuthor">{itemCard.author?.name}</h3>
        </div>
        <p className="cardPrice">{`Год: ${itemCard.year}`}</p>
      </div>
    </div>
  );
};

export default CardBook;
