<?php

// use a $ to make a variable
$con = mysqli_connect('localhost', 'root', 'root', 'unityappletree');

if (mysqli_connect_errno()) {
    echo "1: Connection failed"; //error code #1 - connection failed
    exit();
}

$username = $_POST["username"];
$password = $_POST["password"];

//check if name exists
$namecheckquery = "SELECT username FROM players WHERE username='" . $username . "';";

$namecheck = mysqli_query($con, $namecheckquery) or die("2: Name check query failed"); //error code #2 - name check query failed

if (mysqli_num_rows($namecheck) > 0) {
    echo "3: Name already exists"; //error code #3 - name exists cannot register
    exit();
}

//add user to the table
$salt = "\$5\$rounds=5000\$" . "supersecureencryption" . $username . "\$"; //sha256 encryption. We add a random string and the username to make the password harder to decrypt.
$hash = crypt($password, $salt);
$insertuserquery = "INSERT INTO players (username, hash, salt) VALUES ('" . $username . "', '" . $hash . "', '" . $salt . "');";
mysqli_query($con, $insertuserquery) or die("4: Insert player query failed"); //error code #4 - insert query failed

echo ("0");
?>