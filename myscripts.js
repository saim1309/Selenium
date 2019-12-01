function validate(){
//set all eroor messages to null
document.getElementById("fnameError").textContent=("");
document.getElementById("lnameError").textContent=("");
document.getElementById("addressError").textContent=("");
document.getElementById("cityError").textContent=("");
document.getElementById("phoneError").textContent=("");
document.getElementById("emailError").textContent=("");
document.getElementById("modelError").textContent=("");

//get all values from form's text boxes
var first=document.getElementById("fname").value;
var last=document.getElementById("lname").value;
var address=document.getElementById("address").value;
var city=document.getElementById("city").value;
var phone=document.getElementById("phone").value;
var email=document.getElementById("email").value;
var model=document.getElementById("model").value;
var fnameValid,lnameValid,addressValid,cityValid,phoneValid,emailValid,modelValid;
var reg;

//Make sure all feilds are filled. If not alert user 
if((first=="")||(last=="")||(address=="")||(city=="")||(phone=="")||(email=="")||(model=="")){
alert("Form must be filled");
}
else{

//validate first name against regular expression. if not valid, place an error message
var reg=/^[A-z]{3,}$/;
 if(!reg.test(first)){
document.getElementById("fnameError").textContent="Invalid first name, must be at least 3 letters";
fnameValid=false;}
else{fnameValid=true;}

//validate last name against regular expression. if not valid, place an error message. Here we used the same regular expression that is used for first name 
if(!reg.test(last)){
document.getElementById("lnameError").textContent="Invalid last name, must be at least 3 letters";
lnameValid=false;
}
else{lnameValid=true;}

//Validate address against regular expression
reg=/^(\d+|\d+-\d+)\s[A-z]+\s[A-z\s]*$/;
if(!reg.test(address)){
document.getElementById("addressError").textContent="Invalid address, example: 5-43 gordon st ,or 190 gateway drive";
addressValid=false;}
else{addressValid=true; }

//Validate city
reg=/^[A-z]{3,}$/;
if(!reg.test(city)){
document.getElementById("cityError").textContent="Invalid city name";
cityValid=false;}
else{cityValid=true;}

//validate phone number
reg=/^\d{3}\s{0,1}-\s{0,1}\d{3}\s{0,1}-\s{0,1}\d{4}|\(\d{3}\)\s{0,1}\d{3}\s{0,1}-\s{0,1}\d{4}$/;
if(!reg.test(phone)){
document.getElementById("phoneError").textContent="Invalid phone number 123-123-1234, or (123)123-1234";
phoneValid=false;}
else{phoneValid=true;}

//validate email address
reg=/^\w+@\w+.\w+$/;
if(!reg.test(email)){
document.getElementById("emailError").textContent="Invalid email address";
emailValid=false;}
else{emailValid=true;}

//Validate car model
reg=/^\d{4}\s\w{3,}\s\w{3,}$/;
if(!reg.test(model)){
document.getElementById("modelError").textContent="Invalid car model.e.g. 2012 BMW 328i or 1978 Chrysler LeBaron format";
modelValid=false;}
else{modelValid=true;}

//If all feilds are valid display customer information with the link to jpower website 
if (fnameValid && lnameValid && addressValid && cityValid && phoneValid && emailValid && modelValid){
var str=model.toString().split(" ");
var link="http://www.jdpower.com/cars/"+str[1]+"/"+str[2]+"/"+str[0];
document.getElementById("msg").innerHTML="Name: "+first+" "+last+"<br>Street Address: "+address+"<br>City: "+city+"<br>Phone Number: "+phone+
                                                "<br>Email: "+email+"<br>Model: "+model+"<br><a href="+link+">"+link+"</a>";
document.getElementById("myform").style.display="none";  //make the form invisible

//create json object
var fullName=first+" "+last;
var entry={
"name":fullName,
"address":address,
"city":city,
"phone":phone,
"email":email,
"model":model,
"link":link
};

// add json object to local storage
var existing=JSON.parse(localStorage.getItem("entries"));
if (existing==null){
existing=[];}
localStorage.setItem("entry",JSON.stringify(entry));
existing.push(entry);
localStorage.setItem("entries",JSON.stringify(existing));

}
}
}
//this function is used to display all added cars 
function display(){
document.getElementById("search").style.display="none";
document.getElementById("home").style.display="inline";
var existing=localStorage.getItem("entries");
if (existing==null){
document.getElementById("displayEntries").innerHTML="No Items have been added";
}
else{
var data = JSON.parse(existing);
document.getElementById("displayEntries").innerHTML="Cars Added Recently: <br><br>";
for(var i=0;i<data.length;i++){
document.getElementById("displayEntries").innerHTML+="Name: "+data[i].name+"<br>Street Address: "+data[i].address+"<br>City: "+data[i].city+"<br>Phone Number: "+data[i].phone+
                                                "<br>Email: "+data[i].email+"<br>Model: "+data[i].model+"<br><a href="+data[i].link+">"+data[i].link+"</a><br>";
}
} 
}