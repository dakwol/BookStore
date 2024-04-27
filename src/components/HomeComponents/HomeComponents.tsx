import React, { FC, useEffect, useState } from "react";
import icons from "../../assets/icons/icons";
import { CardData } from "../../models/ICardData";
import CardBook from "../CardBook/CardBook";
import "./styles.scss";
import { Link } from "react-router-dom";
import { RouteNames } from "../../routes";
import GenreApiRequest from "../../api/Genre/Genre";
import { IGenre } from "../../models/IGenre";
import BookApiRequest from "../../api/Book/Book";

const HomeComponents: FC = () => {
  const genreApi = new GenreApiRequest();
  const booksApi = new BookApiRequest();
  const [isGenreArray, setIsGenreArray] = useState<IGenre[]>([]);
  const [isBooksArray, setIsBooksArray] = useState<CardData[]>([]);
  const [categories, setCategories] = useState([
    { id: "", title: "", books: isBooksArray.slice(0, 4) },
  ]);

  useEffect(() => {
    genreApi.list().then((resp) => {
      if (resp.success) {
        setIsGenreArray(resp.data as IGenre[]);

        const updatedCategories =
          resp.data &&
          resp.data.map(async (item: { id: string; name: string }) => {
            const books = await booksApi.list({
              urlParams: `?genreId=${item.id}`,
            });
            if (books.success) {
              return {
                id: item.id,
                title: item.name,
                books: books.data && books.data.slice(0, 4),
              };
            }
          });

        Promise.all(updatedCategories).then((categoriesWithBooks) => {
          const filteredCategories = categoriesWithBooks.filter(
            (category) => category !== undefined
          );
          setCategories(filteredCategories);
        });
      }
    });
  }, []);

  console.log("isGenreArray", isGenreArray);

  return (
    <section>
      <div className="container">
        {categories.map((category, index) => (
          <div key={index} className="categoryContainer">
            <div className="categoryNameContainer">
              <h1 className="categoryTitle">{category.title}</h1>
              <Link
                to={`${RouteNames.CATEGORY}/${encodeURIComponent(
                  JSON.stringify({ id: category.id, title: category.title })
                )}`}
                className="categoryLink"
              >
                Смотреть все
              </Link>
            </div>
            <div className="containerArrayCard">
              {category.books.map((item) => (
                <CardBook key={item.id} itemCard={item} />
              ))}
            </div>
          </div>
        ))}
      </div>
    </section>
  );
};

export default HomeComponents;
