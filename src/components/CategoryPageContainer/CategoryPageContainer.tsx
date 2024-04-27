import React, { FC, useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import Buttons from "../Buttons/Buttons";
import icons from "../../assets/icons/icons";
import { CardData } from "../../models/ICardData";
import CardBook from "../CardBook/CardBook";
import BookApiRequest from "../../api/Book/Book";

interface IProps {
  id: string | undefined;
  title: string | undefined;
}

const CategoryPageContainer: FC<IProps> = ({ id, title }) => {
  const navigation = useNavigate();
  const booksApi = new BookApiRequest();
  const [isBooksArray, setIsBooksArray] = useState<CardData[]>([]);

  useEffect(() => {
    booksApi.getById({ urlParams: `?genreId=${id}` }).then((resp) => {
      if (resp.success) {
        resp.data && setIsBooksArray(resp.data as CardData[]);
      }
    });
  }, []);

  const dataCard: CardData[] = [
    {
      id: 1,
      title: "Dune",
      authorId: "Frank Herbert",
      rating: "87,75 $",
      imagePreview: icons.Picture,
      year: "",
      genreId: "",
    },
  ];
  return (
    <div className="container">
      <Buttons
        ico={icons.ArrowBack}
        text={title || "Назад"}
        onClick={() => navigation(-1)}
        className="backButton"
      />

      <div className="containerArrayCard">
        {isBooksArray.map((item) => (
          <CardBook key={item.id} itemCard={item} direction />
        ))}
      </div>
    </div>
  );
};

export default CategoryPageContainer;
