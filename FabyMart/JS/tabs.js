/***************************/
//@Author: Adrian "yEnS" Mato Gondelle & Ivan Guardado Castro
//@website: www.yensdesign.com
//@email: yensamg@gmail.com
//@license: Feel free to use it, but keep this credits please!					
/***************************/

$(document).ready(function(){
	$(".menu1 > li").click(function(e){
		switch(e.target.id){
			case "news":
				//change status & style menu
				$("#news").addClass("active");
				$("#tutorials").removeClass("active");
				$("#links").removeClass("active");
				$("#online").removeClass("active");
				$("#general").removeClass("active");
				$("#contacts").removeClass("active");
				//display selected division, hide others
				$("div.news").fadeIn();
				$("div.tutorials").css("display", "none");
				$("div.links").css("display", "none");
				$("div.online").css("display", "none");
				$("div.general").css("display", "none");
				$("div.contacts").css("display", "none");
			break;
			case "tutorials":
				//change status & style menu
				$("#news").removeClass("active");
				$("#tutorials").addClass("active");
				$("#links").removeClass("active");
				$("#online").removeClass("active");
				$("#general").removeClass("active");
				$("#contacts").removeClass("active");
				//display selected division, hide others
				$("div.tutorials").fadeIn();
				$("div.news").css("display", "none");
				$("div.links").css("display", "none");
				$("div.online").css("display", "none");
				$("div.general").css("display", "none");
				$("div.contacts").css("display", "none");
			break;
			case "links":
				//change status & style menu
				$("#news").removeClass("active");
				$("#tutorials").removeClass("active");
				$("#links").addClass("active");
				$("#online").removeClass("active");
				$("#general").removeClass("active");
				$("#contacts").removeClass("active");
				//display selected division, hide others
				$("div.links").fadeIn();
				$("div.news").css("display", "none");
				$("div.tutorials").css("display", "none");
				$("div.online").css("display", "none");
				$("div.general").css("display", "none");
				$("div.contacts").css("display", "none");
			break;
			case "online":
				//change status & style menu
				$("#news").removeClass("active");
				$("#tutorials").removeClass("active");
				$("#links").removeClass("active");
				$("#online").addClass("active");
				$("#general").removeClass("active");
				//display selected division, hide others
				$("div.online").fadeIn();
				$("div.news").css("display", "none");
				$("div.tutorials").css("display", "none");
				$("div.links").css("display", "none");
				$("div.general").css("display", "none");
				$("div.contacts").css("display", "none");
			break;
		case "general":
				//change status & style menu
				$("#news").removeClass("active");
				$("#tutorials").removeClass("active");
				$("#links").removeClass("active");
				$("#online").removeClass("active");
				$("#general").addClass("active");
				$("#contacts").removeClass("active");
				//display selected division, hide others
				$("div.general").fadeIn();
				$("div.news").css("display", "none");
				$("div.tutorials").css("display", "none");
				$("div.links").css("display", "none");	
				$("div.online").css("display", "none");	
				$("div.contacts").css("display", "none");
			break;
		case "contacts":
				//change status & style menu
				$("#news").removeClass("active");
				$("#tutorials").removeClass("active");
				$("#links").removeClass("active");
				$("#online").removeClass("active");
				$("#general").removeClass("active");
				$("#contacts").addClass("active");
				//display selected division, hide others
				$("div.contacts").fadeIn();
				$("div.news").css("display", "none");
				$("div.tutorials").css("display", "none");
				$("div.links").css("display", "none");	
				$("div.online").css("display", "none");	
				$("div.general").css("display", "none");
		}
		//alert(e.target.id);
		return false;
	});
});