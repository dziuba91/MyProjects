 function pokaz_nauczycieli(x,y,z) {
	for (var i=1; i<21; i++)
	{
		var t = "nauczyciel"+x+y+i;
		var txt = document.getElementById(t);
		
		if (i==z)
			txt.style.display='block';
		else
			txt.style.display='none';
	}
	
	for (var k=1; k<21; k++)
	{
		t = "opcja"+x+y+k;
		txt = document.getElementById(t);
		txt.selected=false;
	}
}

function dodaj (nazw, imie, id) {
	var txt = document.getElementById("do");
	var txt2 = document.getElementById("do_id");
	txt.value=nazw+' '+imie;
	txt2.value=id;
	
	document.getElementById("klasa_wiad").style.display='none';
	document.getElementById("czy_klasa").value='';
	
	if (nazw=='klasa')
		document.getElementById("czy_klasa").value='klasa';
}

function zmien (x, y) {
	var txt = document.getElementById("do");
	var txt2 = document.getElementById("do_id");
	txt.value=x;
	
	document.getElementById("czy_klasa").value='';
	document.getElementById("klasa_wiad").style.display='none';
	
	txt2.value=y;
}

function pokaz (x) {
	var txt = document.getElementById(x);
	txt.style.display='block';
}

function pokaz_text(x, y) {
	var txt = document.getElementById(y);
	txt.innerHTML=x;
}

function zaznacz(x) {
	var txt = document.getElementById("wszystkie");
	if (txt.checked==true)
		for (var i=0; i<x; i++)
		{
			var t = "uczen"+i;
			var txt = document.getElementById(t);
			txt.checked=true;
		}
	else
		for (var i=0; i<x; i++)
		{
			var t = "uczen"+i;
			var txt = document.getElementById(t);
			txt.checked=false;
		}
}

function isNumberKey(evt){
	var charCode = (evt.which) ? evt.which : event.keyCode
	if (charCode > 31 && (charCode < 48 || charCode > 57))
		return false;
	return true;
}