<?php
// Establish a connection to the database
$con = mysqli_connect('localhost', 'root', 'root', 'unityappletree');

if (mysqli_connect_errno()) {
    echo "1: Connection failed"; // Error code #1 - Connection failed
    exit();
}

$username = $_POST["username"];
$newscore = $_POST["score"];

// Optional Input Validation
if (!isset($username) || !isset($newscore) || empty(trim($username)) || !is_numeric($newscore)) {
    echo "6: Invalid input"; // Error code #6 - Input validation failed
    exit();
}

// Check if username exists
$namecheckquery = "SELECT username, salt, hash, score FROM players WHERE username = ?";
$stmt = $con->prepare($namecheckquery);
$stmt->bind_param("s", $username);
$stmt->execute();
$result = $stmt->get_result();

if ($result->num_rows != 1) {
    echo "5: Either no user with name, or more than one user"; // Error code #5 - Numbers of names matching != 1
    exit();
}

// Update the user's score
$updatequery = "UPDATE players SET score = ? WHERE username = ?";
$stmt = $con->prepare($updatequery);
$stmt->bind_param("is", $newscore, $username);

if (!$stmt->execute()) {
    echo "7: Save query failed"; // Error code #7 - UPDATE query failed
    exit();
}

echo "0"; // Success

?>
