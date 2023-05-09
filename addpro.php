<a href="logout.php">Logout</a>
<?php
session_start();
$servername = "localhost";
$username = "root";
$password = "";
$dbname = "PhoneSale";
$conn = mysqli_connect($servername, $username, $password, $dbname);
if (!$conn) {
    die("Connection failed: " . mysqli_connect_error());
}

if($_SERVER["REQUEST_METHOD"] == "POST"){
    $id = $_POST["proid"];
    $name = $_POST["proname"];
    $price = $_POST["proprice"];
    $image = $_POST["image"];
    $inventory = $_POST["inventory"];
    $sql = "INSERT INTO products (proid, proname, proprice, image, inventory) VALUES ('$id','$name','$price','$image','$inventory')";
    $result = $conn->query($sql);
    exit();

}

if (isset($_GET['delete'])) {
    $username = $_GET['delete'];
    $sql = "DELETE FROM products WHERE proid = '$username'";
    if (mysqli_query($conn, $sql)) {
        echo "Record deleted successfully";
    } else {
        echo "Error deleting record: " . mysqli_error($conn);
    }
}
?>
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Product Manage</title>
        <style>
         {
            padding: 0;
            margin: 0;
            box-sizing: border-box;
        }
		.container {
			display: flex;
			flex-wrap: wrap;
			height: 100vh;
		}
		.item {
			flex-basis: 60%;
			height: 45%;
			box-sizing: border-box;
			border: 1px solid black;
			padding: 10px;
			justify-content: center;
			align-items: center;
            overflow: scroll;
		}
		.btn {
			background-color: green;
			color: yellow !important;
			width: 100px;
			height: 50px;
		}
    </style>
</head>
<body>
<div class = "container">
<div class="item">
    <h1>Delete a product</h1>
	<?php
	$sql = "SELECT * FROM products";
	$result = mysqli_query($conn, $sql);
	if (mysqli_num_rows($result) > 0) {
		echo "<table border='1'><tr><th>Id</th><th>Name</th><th>Price</th><th>Image</th><th>Inventory</th><th>Action</th></tr>";
		while ($row = mysqli_fetch_assoc($result)) {
			echo "<tr><td>" . $row["proid"] . "</td><td>" . $row["proname"] . "</td><td>" . $row["proprice"] . "</td><td>" . $row["image"] . "</td><td>" . $row["inventory"] . "</td>
            <td><a href='addpro.php?delete=" . $row["proid"] . "'>Delete</a></td></tr>";
		}
		echo "</table>";
	} else {
		echo " 0 results";
	}
	?>
    </div>
    <div class = "item">
    <h1>Add a product</h1>

        <form action="addpro.php" method="post">
        <div class="form-group">
                    <label>Product id</label>
                    <input type="text" name="proid" class="input-area" required>
                </div>    
                <div class="form-group">
                    <label>Product name</label>
                    <input type="text" name="proname" class="input-area" required>
                </div>
                <div class="form-group">
                    <label>Product price</label>
                    <input type="text" name="proprice" class="input-area" required>
                </div>    
                <div class="form-group">
                    <label>Product image</label>
                    <input type="text" name="image" class="input-area" required>
                </div>
                <div class="form-group">
                    <label>Inventory</label>
                    <input type="text" name="inventory" class="input-area" required>
                </div>    

                <div class="form-group1">
                    <input type="submit" class="login-btn" value="Add">
                </div>
                
        </form>
    </div>
    </div>
</body>
</html>