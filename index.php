<?php
    session_start();

    if (!isset($_SESSION['loggedin'])) {
        header("Location: customer.php");
        exit;
    }
    $servername = "localhost";
$username = "root";
$password = "";
$dbname = "PhoneSale";

$conn = mysqli_connect($servername, $username, $password, $dbname);

?>
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <title>Trang chủ - Danh sách sản phẩm</title>
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.16.0/umd/popper.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta2/css/all.min.css" integrity="sha512-YWzhKL2whUzgiheMoBFwW8CKV4qpHQAEuvilg9FAn5VJUDwKZZxkJNuGM4XkWuk94WCrrwslk8yWNGmY1EduTA==" crossorigin="anonymous" referrerpolicy="no-referrer" />
    <style>
        td {
            vertical-align: middle;
        }
        img {
            max-height: 100px;
        }
        #cart {
            margin-left: auto;
            padding-right: 20px;
        }
        #quantity {
            width: 30px;
        }
    </style>
</head>
<body>

<div class="container">
    <div class="row justify-content-center">
        <div class="col col-md-10">
            <h3 class="my-4 text-center">Product List</h3>
            <div class="d-flex justify-content-between">
                <!-- <a class="btn btn-sm btn-secondary mb-4" href="add_product.php">Add Product</a> -->
                <a id="cart" href="cart.php"><i class="fas fa-shopping-cart"></i></a>
                <a href="logout.php">Logout</a>
            </div>
            <table class="table-bordered table table-hover text-center">
                <tr>
                    <th>Image</th>
                    <th>Name</th>
                    <th>Price</th>
                    <th>Quantity</th>
                    <th>Actions</th>
                </tr>
                <?php
                        $sql_product = mysqli_query($conn, 'SELECT * FROM products ORDER BY proid DESC'); #lay san pham theo id giam dan
                        while($row = mysqli_fetch_array($sql_product)) {
                ?>
                <tr>
                <form action="bill.php?action=add" method="POST">
                    <td class="align-middle"><img src="image/<?=$row['image'] ?>"></td>
                    <td class="align-middle"><?=$row['proname'] ?></td>
                    <td class="align-middle" name="priceItem"><?php echo $row['proprice'] ?>$</td>
                    <td class="align-middle"><input min="0" type="number" id="quantity" name="quantity[<?=$row['proid']?>]" /></td>
                    <td class="align-middle">
                       <input value="Add" name="submit" type="submit">
                    </td>
                </form>
                </tr>
                <?php
                        }
                    ?>
                    
            </table>
        </div>
    </div>
</div>

    
</body>
</html>