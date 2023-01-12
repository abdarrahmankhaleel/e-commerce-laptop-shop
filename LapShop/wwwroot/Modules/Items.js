var ClsItems = {
    GetAll: function () {
        Helper.AjaxCallGet("https://localhost:7159/api/items",function (data) {
            let htmlData = "";
            for (var i = 0; i < data.data.length; i++) {
                htmlData+=ClsItems.DrawItem( data.data[i]);
            }
            
            $("#itemArea").html(htmlData);
            }, function () { });
    },
    DrawItem: function (item) {
        let rowData = `
                                                <div class="col-xl-3 col-6 col-grid-box">
                                                    <div class="product-box">
                                                        <div class="img-wrapper">
                                                            <div class="front">
                                                                <a href="#"><img src="${item.imageName}" class="img-fluid blur-up lazyload bg-img" alt=""></a>
                                                            </div>
                                                            <div class="back">
                                                                <a href="#"><img src="../assets/images/pro3/36.jpg" class="img-fluid blur-up lazyload bg-img" alt=""></a>
                                                            </div>
                                                            <div class="cart-info cart-wrap">
                                                                <button data-toggle="modal" data-target="#addtocart" title="Add to cart">
                                                                    <i class="ti-shopping-cart"></i>
                                                                </button> <a href="javascript:void(0)" title="Add to Wishlist">
                                                                    <i class="ti-heart" aria-hidden="true"></i>
                                                                </a> <a href="#" data-toggle="modal" data-target="#quick-view" title="Quick View">
                                                                    <i class="ti-search" aria-hidden="true"></i>
                                                                </a> <a href="compare.html" title="Compare">
                                                                    <i class="ti-reload" aria-hidden="true"></i>
                                                                </a>
                                                            </div>
                                                        </div>
                                                        <div class="product-detail">
                                                            <div>
                                                                <div class="rating"><i class="fa fa-star"></i> <i class="fa fa-star"></i> <i class="fa fa-star"></i> <i class="fa fa-star"></i> <i class="fa fa-star"></i></div>
                                                                <a href="product-page(no-sidebar).html">
                                                                    <h6>  ${item.itemName}</h6>
                                                                </a>
                                                                <p>
                                                                  ${item.description}
                                                                </p>
                                                                <h4>${item.salesPrice}</h4>
                                                                <ul class="color-variant">
                                                                    <li class="bg-light0"></li>
                                                                    <li class="bg-light1"></li>
                                                                    <li class="bg-light2"></li>
                                                                </ul>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

`

        return rowData;
    }
}
