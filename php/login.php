<?php

$con = mysqli_connect('localhost', 'root', 'root', 'unityappletree');

if (mysqli_connect_errno()) {
    echo "1: Connection failed"; // Error code #1 - connection failed
    exit();
}

$username = $_POST["username"];
$password = $_POST["password"];

// Check if name exists
$namecheckquery = "SELECT username, salt, hash, score FROM players WHERE username = ?";
$stmt = $con->prepare($namecheckquery);
$stmt->bind_param("s", $username); // "s" specifies the variable type: string
$stmt->execute();
$result = $stmt->get_result();

if ($result->num_rows !== 1) {
    echo "5: Either no user with name, or more than one user"; // Error code #5 - numbers of names matching != 1
    exit();
}

// Get login info from query
$existinginfo = $result->fetch_assoc();
$salt = $existinginfo["salt"];
$hash = $existinginfo["hash"];

// Hash the password with the salt
$loginhash = crypt($password, $salt);

if ($hash !== $loginhash) {
    echo "6: Incorrect password or username"; // Error code #6 - password does not hash to match table
    exit();
}

echo "0\t" . $existinginfo["score"];

// Close statement and connection
$stmt->close();
$con->close();

?>
