<?php
     $servername = "localhost";
     $username = "root";
     $password = "";
     $dbname = "registrationform";

     try
     {
          $conn = mysqli_connect($servername, $username, $password, $dbname);
     }
     catch(MySQLi_Sql_Exception $ex)
     {
          echo("error in connection");
     }

     if(isset($_POST['submit']))
     {
          $visitorid = $_POST['visitor_id'];
          $firstname = $_POST['fname'];
          $lastname = $_POST['lname'];
          $type = $_POST['type'];
          #$platenumber = $_POST['plate'];
          $status = "Filled";
          
           if (empty($visitorid)) 
           {
             echo 
               "<script type='text/javascript'>".
               "window.alert('Visitor ID can't be Empty!!');".
               "</script>";
           }
           else
           {
             if ($type == "2 Wheels - Bicycle") {
               $platenumber = "Bicycle";
             }
             else
             {
               $platenumber = $_POST['plate'];
             }

             $query = "SELECT COUNT(*) FROM visitor_archive WHERE visitor_id = '".$visitorid."'";
             $result = mysqli_query($conn, $query);

             if ($row=mysqli_fetch_array($result))
             {  
               try
               {
                 $register_query = "UPDATE `visitor_archive` SET `fname`='$firstname',`lname`='$lastname',`type`='$type',`plate_num`='$platenumber',`status`='$status' WHERE visitor_id ='$visitorid'";
                 $register_result = mysqli_query($conn, $register_query);

                 if($register_result)
                 {
                   if(mysqli_affected_rows($conn) > 0)
                   {
                     #echo 
                     #"<script type='text/javascript'>".
                     #"window.alert('Registration Successful!!');".
                     #"</script>";
                   }
                   else
                   {
                     #echo 
                     #"<script type='text/javascript'>".
                     #"window.alert('Visitor ID not Found!!');".
                     #"</script>";
                   }
                 }
                 
                 $register_query2 = "UPDATE `visitor_logs` SET `fname`='$firstname',`lname`='$lastname',`type`='$type',`plate_num`='$platenumber',`status`='$status' WHERE visitor_id ='$visitorid'";
                 $register_result2 = mysqli_query($conn, $register_query2);
                 
                 if($register_result2)
                 {
                   if(mysqli_affected_rows($conn) > 0)
                   {
                     echo 
                       "<script type='text/javascript'>".
                       "window.alert('Registration Successful!!');".
                       "</script>";
                   }
                   else
                   {
                     echo 
                       "<script type='text/javascript'>".
                       "window.alert('Visitor ID not Found!!');".
                       "</script>";
                   }
                 }
                 
                 
               }
               catch(Exception $ex)
               {
                 echo("error".$ex->getMessage());
               }
             }
             else
             {
               echo 
                 "<script type='text/javascript'>".
                 "window.alert('Visitor ID not Found!!');".
                 "</script>";
             }
           }                    
         }
?>

<!DOCTYPE html>
<html lang="en">
<head>

     <title>QCU | Vehicle Records Management System</title>

     <meta charset="UTF-8">
     <meta http-equiv="X-UA-Compatible" content="IE=Edge">
     <meta name="description" content="">
     <meta name="keywords" content="">
     <meta name="author" content="">
     <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1">
     <link rel="icon" href="images/qcu logo.png" type="icon">
     <link rel="stylesheet" href="css/bootstrap.min.css">
     <link rel="stylesheet" href="css/font-awesome.min.css">
     <link rel="stylesheet" href="css/owl.carousel.css">
     <link rel="stylesheet" href="css/owl.theme.default.min.css">

     <!-- MAIN CSS -->
     <link rel="stylesheet" href="css/style.css">

</head>
<body id="top" data-spy="scroll" data-target=".navbar-collapse" data-offset="50">

     <!-- PRE LOADER -->
     <section class="preloader">
          <div class="spinner">
               <span class="spinner-rotate"></span>
          </div>
     </section>


     <!-- MENU -->
     <section class="navbar custom-navbar navbar-fixed-top" role="navigation">
          <div class="container">

               <div class="navbar-header">
                    <button class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
                         <span class="icon icon-bar"></span>
                         <span class="icon icon-bar"></span>
                         <span class="icon icon-bar"></span>
                    </button>

                    <!-- lOGO TEXT HERE -->
                    <a href="index.php" class="navbar-brand">QCU : VRMS</a>
               </div>

               <!-- MENU LINKS -->
               <div class="collapse navbar-collapse">
                    <ul class="nav navbar-nav navbar-nav-first">
                         <li><a href="index.php">Home</a></li>
                         <li class="active"><a href="VFF.php">Visitor Fill up Form</a></li>
                    </ul>
               </div>

          </div>
     </section>

     <section class="section-background">
          <div class="container">
               <div class="row">
                    <div class="col-lg-12 col-xs-12">
                         <div class="row">
                              <div class="col-sm-12">
                                   <div class="text-center">
                                        <video width="100%" height="auto" controls>
                                             <source src="images/qcuflow.mp4" type="video/mp4">
                                        </video>
                                   </div>
                              </div>
                         </div>
                    </div>

                    <div class="col-lg-9 col-xs-12">
                         <div class="row">
                              <div class="col-sm-12">
                                   <div class="text-center modal-content">
                                        <h2>Welcome to Quezon City University!</h2>
                                        <br>
                                        <h3>Hello and Good day, just click the proceed button to fill up the Visitor Monitoring Form, which is needed for the system to record Visitors Information, be sure to input the correct details before you exit</h3>
                                   </div>
                              </div>
                         </div>
                    </div>     

                    <div class="col-lg-3 pull-right col-xs-12 modal-content">
                         <div class="form">
                              <form action="#">
                                   <div class="form-group">
                                     <label class="control-label"><h2>Reminders:</h2></label>
                                   </div>
                              </form>
                         </div>

                         <br>

                         <label class="control-label"><h4>Rules and Regulations inside the campus</h4></label>

                         <ul class="list text-justify">
                              <li>&nbsp Data Privacy Clause: By completing this form, I hereby agree that the researchers may collect, use, disclose, and process my personal data to this Visitor Monitoring Form.</li>

                              <li>&nbsp Bicycles Owners/Users of such shall still requires to fill up the Visitor Monitoring Form but doesn't requires to input a Plate Number. Owners/Users are responsible in securing their bicycles inside the premises.</li>

                              <li>&nbsp Park at your own risk. The system and the management are not responsible for any theft or damage to your vehicles. Lock your vehicle. Secure and do not leave your valuables in plain sight.</li>
<br>
                         </ul>
                    </div>

                    <div class="col-lg-9 col-xs-12">
                         <!--Content-->
                         <div class="modal-content">
                              <!--Header-->
                              <div class="modal-header d-flex justify-content-center">
                                   <h2 class="modal-title w-100 font-weight-bold  text-center">Visitor Monitoring Form</h2>
                              </div>

                              <form method="POST" name="regForm">
                                   <!--Body-->
                                   <div class="modal-body">

                                        <div class="md-form mb-5">
                                             <i class="fas fa-id-card-alt prefix grey-text"></i>
                                             <label>
                                                  Visitor ID#
                                             </label>
                                             <input onkeyup="numbersOnly(this)" type="text" id="visitor_id" name="visitor_id" class="text-center form-control validate" placeholder="Visitor ID#" value="" required>
                                        </div>

                                        <div class="row">
                                             <div class="col-md-6">
                                                  <i class="fas fa-id-card-alt prefix grey-text"></i>
                                                  <label>
                                                       First Name
                                                  </label>
                                                  <input onkeyup="lettersOnly(this)" type="text" id="fname" name="fname" class="text-center form-control validate" placeholder="First Name" value="" required>
                                             </div>

                                             <div class="col-md-6">
                                                  <i class="fas fa-id-card-alt prefix grey-text"></i>
                                                  <label>
                                                       Last Name
                                                  </label>
                                                  <input onkeyup="lettersOnly(this)" type="text" id="lname" name="lname" class="text-center form-control validate" placeholder="Last Name" value="" required>
                                             </div>
                                        </div>
                                        
                                        <div class="row">
                                             <div class="col-md-6">
                                                  <i class="fas fa-id-card-alt prefix grey-text"></i>
                                                  <label>
                                                       Vehicle Type
                                                  </label><br>
                                                  <select class="text-center form-control validate" name="type" id="type" autocomplete="off" required>
														<option selected value="Choose">Vehicle Type</option>
                                                        <option value="2 Wheels - Bicycle">2 Wheels - Bicycle</option>
                                                        <option value="2 Wheels - Motorcycle">2 Wheels - Motorcycle</option>
                                                        <option value="4 Wheels - Sedan">4 Wheels - Sedan</option>
                                                        <option value="4 Wheels - SUV / AUV">4 Wheels - SUV / AUV</option>
                                                        <option value="4 Wheels - Van">4 Wheels - Van</option>
                                                  </select>
                                             </div>

                                             <div class="col-md-6">
                                                  <i class="fas fa-id-card-alt prefix grey-text"></i>
                                                  <label>
                                                       Plate Number
                                                  </label>
                                                  <input type="text" id="plate" name="plate" class="text-center form-control validate" placeholder="Plate Number" value="" required>
                                             </div>
                                        </div>
<br>
                                        <div class="row">
                                             <div class="reminders text-justify" style="background: #D4EDDA; color: #A94442; padding: 10px; width: 95%; border-radius: 5px; margin: 20px auto;" >
                                                  <p>
                                                  &nbsp &nbsp &nbsp Please be advised that visitors need to fill up all of the required information above to be able for the system to record visitor data. All infomation provided by the visitors are considered vital and will not be used on other proccess outside the system.
                                                  </p>
                                             </div>
                                        </div>
                                   </div>

                                   <!--Footer-->
                                   <div class="modal-footer flex-center">
                                        <div class="text-center">
                                             <button type="submit" id="submit" name="submit" class="third btn btn-default btn-primary" data-togle="modal" data-target="#myModal">
                                                  Submit
                                             </button>
                                        </div>
                                   </div>
                              </form>
                         </div>
                         <!--/.Content-->
                    </div>
               </div>
          </div>
     </section>

     <!-- FOOTER -->
     <footer id="footer">
          <div class="container">
               <div class="row">

                    <div class="col-md-4 col-sm-6">
                         <div class="footer-info">
                              <div class="section-title">
                                   <h2>QUEZON CITY UNIVERSITY</h2>
                              </div>
                              <address>
                                   <p>673 Quirino Highway <br>San Bartolome, Novaliches Quezon City</p>
                              </address>

                              <ul class="social-icon">
                                   <li><a href="#" class="fa fa-facebook-square" attr="facebook icon"></a></li>
                                   <li><a href="#" class="fa fa-twitter"></a></li>
                                   <li><a href="#" class="fa fa-instagram"></a></li>
                              </ul>

                              <div class="copyright-text"> 
                                   <p>Copyright &copy; 2022 BSIT | SBIT-4L</p>
                                   <p>Developed by: Source Code Messiah</p>
                              </div>
                         </div>
                    </div>

                    <div class="col-md-4 col-sm-6">
                         <div class="footer-info">
                              <div class="section-title">
                                   <h2>Contact Info</h2>
                              </div>
                              <address>
                                   <p> (02) 8806-3049</p>
                                   <p><a href="https://qcu.edu.ph/">https://qcu.edu.ph/</a></p>
                              </address>

                              <div class="footer_menu">
                                   <h2>Quick Links</h2>
                                   <ul>
                                        <li><a href="index.php">Home</a></li>
                                        <li><a href="VFF.php">Visitor Fill up Form</a></li>
                                   </ul>
                              </div>
                         </div>
                    </div>           
               </div>
          </div>
     </footer>

     <!-- SCRIPTS -->
     <script src="js/jquery.js"></script>
     <script src="js/bootstrap.min.js"></script>
     <script src="js/owl.carousel.min.js"></script>
     <script src="js/smoothscroll.js"></script>
     <script src="js/custom.js"></script>
     <script type="text/javascript">
          //Disable letters
          function numbersOnly(input) 
          {
              var regex = /[^0-9]/gi;
              input.value = input.value.replace(regex, "");
          }
    
          //Disable numbers
          function lettersOnly(input) 
          {
              var regex = /[^ a-z]/gi;
              input.value = input.value.replace(regex, "");
          }
     </script>
     <script src="js/sweetalert.min.js"></script>
     <script src="https://cdn.jsdelivr.net/npm/sweetalert2@10.10.1/dist/sweetalert2.all.min.js"></script>
     <link rel='stylesheet' href='https://cdn.jsdelivr.net/npm/sweetalert2@10.10.1/dist/sweetalert2.min.css'>

     <script type="text/javascript">
          $(function () {
               $("#type").change(function () {
                    if ($(this).val() == "2 Wheels - Bicycle") 
                    {
                         $("#plate").attr("disabled", "disabled");
                         $('#plate').val('Bicycle'); 
                    } 
                    else 
                    {
                         $("#plate").removeAttr("disabled");
                         $('#plate').val(''); 
                 }
               });
          });
     </script>

    <script src="https://www.google.com/recaptcha/api.js" async defer></script>
</body>
</html>