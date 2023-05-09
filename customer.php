<?php
session_start();
if(isset($_SESSION["loggedin"]) && $_SESSION["loggedin"] === true){
    header("location: index.php");
    exit;
}

$servername = "localhost";
$username = "root";
$password = "";
$dbname = "PhoneSale";

$conn = mysqli_connect($servername, $username, $password, $dbname);

if($_SERVER["REQUEST_METHOD"] == "POST"){
 
    $username = $_POST["username"];
    $password = mysqli_real_escape_string($conn,$_POST["password"]);
    $sql = "SELECT * FROM users WHERE username = '".$username."' and userpassword = '".$password."';";
    $result = $conn->query($sql);
    $count = mysqli_num_rows($result);
    
    if($count){
        $_SESSION["loggedin"] = true;
        $sql = "SELECT userphone FROM users WHERE username = '".$username."' and userpassword = '".$password."';";
        $result = $conn->query($sql);
        $row = mysqli_fetch_assoc($result);
        $_SESSION["userphone"] = $row["userphone"];
        header("location: index.php");
    } else {
        $error = "Invalid username or password";
    }
}
?>
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <title>Login</title>
    <link rel="stylesheet" href="login.css">
    <style >
        body{ 
            background-color: #2C2E47;
            font: 14px sans-serif; 
        }
        
    </style>
</head>
    
<body >
        <div class="login-wrapper login-text">
            <h2>Customer Login</h2>
            <p>Enter your information</p>
            <?php 
            if(isset($error)){
                echo "<div class='login-error'>" . $error . "</div>";
            }
            ?> 
            <p id = "erro"></p>
            <form action="customer.php" method="post">
                <div class="form-group">
                    <label>Username</label>
                    <input type="text" name="username" class="input-area" required>
                </div>    
                <div class="form-group">
                    <label>Password</label>
                    <input type="text" name="password" class="input-area" required>
                </div>
                <a href="seller.php" style="float: right; color: #fff">Seller?  </a>
                <div class="form-group1">
                    <input type="submit" class="login-btn" value="Login">
                </div>
            </form>
        </div>
</body>
</html>  
