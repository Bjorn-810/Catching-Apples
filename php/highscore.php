<?php
$con = mysqli_connect('localhost', 'root', 'root', 'unityappletree');

if (mysqli_connect_errno())
{
    echo "1: Connection failed"; //error code #1 = connection failed
    exit();
}

$username = $_POST["username"];
$newscore = $_POST["score"];

$namecheckquery = "SELECT username, salt, hash, score FROM players WHERE username='" . $username . "';";

$namecheck = mysqli_query($con, $namecheckquery) or die("2: Name check query failed"); //error code #2 - name check query failed
if (mysqli_num_rows($namecheck) != 1)
{
    echo "5: Either no user with name, or more than one user"; //error code number #5 - numbers of names matching != 1
    exit();
}

$updatequery = "UPDATE players SET score = " . $newscore . " WHERE username = '" . $username . "';";
mysqli_query($con, $updatequery) or die("7: Save query failed"); // error code #7 - UPDATE query failed

echo "0";
?>
