using TheGrace.Domain.Entities.Builder.ProductCategoryBuilderPattern;

namespace TheGrace.Domain.Entities.Builder.ProductBuilderPattern;

public interface IProductBuilder
{
    IProductBuilder SetProductCategory(ProductCategory productCategory);

    IProductBuilder SetName(string name);

    IProductBuilder SetCode(string code);

    IProductBuilder SetDescription(string description);

    IProductBuilder SetImage(string image);

    IProductBuilder SetPrice(decimal price);

    IProductBuilder SetQuantity(int quantity);

    IProductBuilder SetSoftDelete(bool isInActive);

    IProductBuilder SetCreatedBy(string createdBy, DateTimeOffset createdAt);

    IProductBuilder SetUpdatedBy(string updatedBy, DateTimeOffset updatedAt);

    Product Build();
}
