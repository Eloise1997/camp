document.addEventListener('DOMContentLoaded', function() {
    document.getElementById('btnAdd2Cart').addEventListener('click', function(event) {
        let btn = event.target;
        let url='/api/CartApi';
        let productId = btn.dataset.productid; // dataset 必需小寫
        let qty = 1;
        console.log('productId', productId);
        let data = {
            productId: productId,
            quantity: qty
        };
        console.info('data', JSON.stringify(data));
        fetch(url, {
            method: 'POST',
            body: JSON.stringify(data),
            headers: new Headers({
                'Content-Type': 'application/json'
            })
        }).then(function(response) {
            return response.json();
        }).then(function(result) {
            console.log("return cart=", result); // 回傳Cart明細
            // alert('已加入購物車');
            // 更新購物車數量
            updateCartInfo(result);
        }).catch(function(error) {
            console.log('Request failed', error);
        });
    });
});