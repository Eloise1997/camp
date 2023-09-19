function updateCartInfo(cart) {
    console.log('updateCartInfo', cart);
    console.log('cart items length', cart.items.length);

    let count = cart.items.length == 0
        ? 0
        : cart.items
            .map(function (item) {
                return item.qty;
            })
            .reduce(function (prev, curr) {
                return prev + curr;
            });
    let panel = document.getElementById('cartIcon');
    panel.innerHTML = `cart ( ${count} )`;
}