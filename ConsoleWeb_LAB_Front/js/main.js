
const productBox = document.querySelector('#productsBox');

loadProducts();

function createElement(product){
    const elementStr = ` <div class="col mb-5">
    <div class="card h-100">
        <div class="badge bg-dark text-white position-absolute" style="top: 0.5rem; right: 0.5rem"></div>
        <img class="card-img-top" src="https://dummyimage.com/450x300/dee2e6/6c757d.jpg" alt="...">
        <div class="card-body p-4">
            <div class="text-center">
                <!-- Product name-->
                <h5 class="fw-bolder">${product.name}</h5>
                <!-- Product price-->
                $${product.price}
            </div>
        </div>
        <div class="card-footer p-4 pt-0 border-top-0 bg-transparent">
            <div class="text-center" ><a data-id="${product.id}" class="btn btn-outline-dark mt-auto add-to-wishlist" href="#">Add To Wishlist</a></div>
        </div>
    </div>
    </div>`
    return elementStr;
}


function loadProducts(){
    fetch("http://localhost:50711/products")
    .then(response =>response.json())
    .then(datas=>{
        datas.forEach(data => {
            let product = createElement(data);
            productBox.innerHTML+=product;
        });
    })
}


document.querySelector(".modal-body form").addEventListener("submit", function(e){
    e.preventDefault();

    const productName = document.getElementById("nameInput").value;
    const productPrice = document.getElementById("priceInput").value;

    const productData = {
        name: productName,
        price: productPrice
    };

    fetch("http://localhost:50711/products", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(productData)
    })
    .then(response => response.json())
    .then(result => {
        console.log(result);
    });

    document.getElementById("nameInput").value = "";
    document.getElementById("priceInput").value = "";
});
