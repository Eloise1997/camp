
AOS.init();

//固定按鈕
window.onscroll = function () {
  if (window.pageYOffset > 400) {
    document.getElementById("gotop").style.display = "block";
    document.getElementById("booking").style.display = "block";
  } else {
    document.getElementById("gotop").style.display = "none";
    document.getElementById("booking").style.display = "none";
  }
};

function scrollToTop() {
  window.scrollTo({ top: 0, behavior: 'smooth' });
}


//最新消息自動跳動
  const descriptions = document.querySelectorAll('.description');
  let currentIndex = 0;

  function updateCarousel() {
    descriptions.forEach((description, index) => {
      if (index === currentIndex) {
        description.classList.add('active');
      } else {
        description.classList.remove('active');
      }
    });

    currentIndex = (currentIndex + 1) % descriptions.length;
  }
  updateCarousel();

  setInterval(updateCarousel, 5000); 


  document.addEventListener("DOMContentLoaded",function(){
    const imageContainers = document.querySelectorAll('.image-container');

    imageContainers.forEach(container => {
      container.addEventListener('mouseenter', () => {
  
        const image = container.querySelector('img');
        image.style.filter = 'blur(2px) brightness(0.5)';
        image.style.transform = 'scale(1.1)';
  
        const overlay = container.querySelector('.overlay');
        overlay.style.opacity = '1';
      });
  
      container.addEventListener('mouseleave', () => {
  
        const image = container.querySelector('img');
        image.style.filter = '';
        image.style.transform = '';
  
        const overlay = container.querySelector('.overlay');
        overlay.style.opacity = '0';
      });
    });
  });


