<?php

// Establish a connection to the database
$con = mysqli_connect('localhost', 'root', 'root', 'unityappletree');

if (mysqli_connect_errno()) {
    echo "1: Connection failed"; // Error code #1 - Connection failed
    exit();
}

$username = $_POST["username"];
$password = $_POST["password"];

// Validate input
if (!isset($username) || !isset($password) || empty(trim($username)) || empty(trim($password))) {
    echo "6: Invalid input"; // Error code #6 - Input validation failed
    exit();
}

// Check if username exists
$namecheckquery = "SELECT username FROM players WHERE username = ?";
$stmt = $con->prepare($namecheckquery);
$stmt->bind_param("s", $username);
$stmt->execute();
$result = $stmt->get_result();

if ($result->num_rows > 0) {
    echo "3: Name already exists"; // Error code #3 - Name exists, cannot register
    exit();
}

// Add user to the table
$salt = "\$5\$rounds=5000\$" . "supersecureencryption" . $username . "\$"; // SHA256 encryption. A random string and the username are added to make the password harder to decrypt.
$hash = crypt($password, $salt);

$insertuserquery = "INSERT INTO players (username, hash, salt) VALUES (?, ?, ?)";
$stmt = $con->prepare($insertuserquery);
$stmt->bind_param("sss", $username, $hash, $salt);

if (!$stmt->execute()) {
    echo "4: Insert player query failed"; // Error code #4 - Insert query failed
    exit();
}

echo "0"; // Success

?>
