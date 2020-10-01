var vocs = {'read':0, 'play':0, 'book':0};

function loagPage() {

for([key, val] of Object.entries(vocs)) {
	  window.location = 'https://youglish.com/pronounce/'+key+'/english'; 
          val = document.getElementById("ttl_total").textContent;
  	  alert(key + ' ' + val);

	}
}

loagPage();


