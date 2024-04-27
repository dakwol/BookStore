import React, { FC } from "react";
import Header from "../../components/Header/Header";
import { useParams } from "react-router-dom";
import CategoryPageContainer from "../../components/CategoryPageContainer/CategoryPageContainer";

const CategoryPage: FC = () => {
  const { category } = useParams();
  const categoryObject = JSON.parse(decodeURIComponent(category || "") || "{}");

  return (
    <div>
      <CategoryPageContainer
        title={categoryObject.title}
        id={categoryObject.id}
      />
    </div>
  );
};

export default CategoryPage;
