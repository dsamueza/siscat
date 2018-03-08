
 // Closes the sidebar menu
 $("#menu-close").click(function(e) {
   $(".content-menu").toggleClass("active");
   $(".header-menu").toggleClass("active");
   $(".btn-menu").toggleClass("active");
 });
 // Opens the sidebar menu
 $("#menu-open").click(function(e) {

   e.preventDefault();
      $(".content-menu").toggleClass("active");
      $(".header-menu").toggleClass("active");
      $(".btn-menu").toggleClass("active");
 });
