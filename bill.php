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
    <title>Shopping bill</title>
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.16.0/umd/popper.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta2/css/all.min.css"
        integrity="sha512-YWzhKL2whUzgiheMoBFwW8CKV4qpHQAEuvilg9FAn5VJUDwKZZxkJNuGM4XkWuk94WCrrwslk8yWNGmY1EduTA=="
        crossorigin="anonymous" referrerpolicy="no-referrer" />
    <link rel="stylesheet" href="css/style.css">
</head>

<body>
    <?php
    if (!isset($_SESSION["bill"])) {
        $_SESSION["bill"] = array();
    }
    $error = false;
    $success = false;
    if (isset($_GET['action'])) {
        if ($_GEt['action'] = 'add') {
            foreach ($_POST['quantity'] as $proid => $quantity) {
                if ($quantity <= 0) {
                    unset($_SESSION["bill"][$proid]);
                } else {
                    if ($add) {
                        $_SESSION["bill"][$proid] += $quantity;
                    } else {
                        $_SESSION["bill"][$proid] = $quantity;
                    }
                }
            }
            header("Location: ./bill.php");
        }
        if ($_GEt['action'] = 'delete') {
            if (isset($_GET['proid'])) {
                unset($_SESSION["bill"][$_GET["proid"]]);
            }
            header("Location: ./bill.php");
        }
    }
    if (!empty($_SESSION["bill"])) {
        $ids = array_map(function ($proid) {
            return "'" . $proid . "'";
        }, array_keys($_SESSION["bill"]));
        $idList = implode(",", $ids);
        $product = mysqli_query($conn, "SELECT * FROM `products` WHERE `proid` IN (" . $idList . ")");
    }
    ?>
    <div class="container">
        <div class="row justify-content-center">
            <div class="col col-md-10">
                <h3 class="my-4 text-center">bill</h3>
                <div class="d-flex justify-content-between">
                    <a class="btn btn-sm btn-secondary mb-4" href="index.php">Return Product List</a>
                    <a href="logout.php">Logout</a>
                </div>
                <table class="table-bordered table table-hover text-center">
                    <tr>
                        <th>Number</th>
                        <th>Image</th>
                        <th>Name</th>
                        <th>Price</th>
                        <th>Quantity</th>
                        <th>Total</th>
                        <th>Action</th>
                    </tr>
                    <?php
                    if (!empty($product)) {
                        $total = 0;
                        $num = 1;
                        while ($row = mysqli_fetch_array($product)) {
                            ?>
                            <tr>
                                <form action="bill.php?action=submit" method="POST">
                                    <td class="align-middle">
                                        <?= $num ?>
                                    </td>
                                    <td class="align-middle"><img style="width : 100%" src="image/<?= $row['image'] ?>"></td>
                                    <td class="align-middle">
                                        <?php echo $row['proname'] ?>
                                    </td>
                                    <td class="align-middle">
                                        <?= $row['proprice'] ?>$
                                    </td>
                                    <?php
                                    $quantity = '';
                                    if (isset($_POST['quantity'][$row['proid']])) {
                                        $quantity = $_POST['quantity'][$row['proid']];

                                    }
                                    if (isset($_GET['addbill'])) {
                                        foreach ($_POST["quantity"] as $proid => $quantity) {
                                            $_SESSION["bill"][$proid] = $quantity;
                                        }
                                    }
                                    ?>
                                    <td class="align-middle"><input type="number" min="0" id="quantity"
                                            value="<?= $_SESSION["bill"][$row['proid']] ?>" name="quantity[<?= $row['proid'] ?>]" />
                                    </td>
                                    <td class="align-middle">
                                        <?= number_format($row['proprice'] * $_SESSION["bill"][$row['proid']], 0, ",", ".") ?>$
                                    </td>
                                    <td class="align-middle"><a
                                            href="bill.php?action=delete&proid=<?= $row['proid'] ?>">Delete</a></td>

                                    <?php
                                    $total += $row['proprice'] * $_SESSION["bill"][$row['proid']];
                                    $num++;
                        }
                        ?>
                            <?php
                    }
                    ?>
                    <tr>
                        <td colspan="7" class="">
                            <input class="seetotal" value="Buy" type="submit" name="update_click">
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
</body>

</html>