<?php 
$name = $_POST['name'];
$email = $_POST['email'];
$phone = $_POST['phone'];
$company = $_POST['company'];
$message = $_POST['message'];
$formcontent="From: $name \n Phone: $phone \n Company: $company \n Message: $message";
$recipient = "rianree20@gmail.com";
$subject = "Contact Form";
$mailheader = "From: $email \r\n";
mail($recipient, $subject, $formcontent, $mailheader) or die("Error!");
//echo "Thank You!";
header('Location: thank-you.html');
?>