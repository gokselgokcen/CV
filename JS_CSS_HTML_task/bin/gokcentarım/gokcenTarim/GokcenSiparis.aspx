<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GokcenSiparis.aspx.cs" Inherits="gokcenTarim.GokcenSiparis" %>

<!DOCTYPE html>
<html lang="en">

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Sipariş Sayfası</title>
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet">
    <style>
        .bg-green {
            background-color: #85ca8e;
            /* Bu renk kodu, teal-100 renk temasına karşılık gelir. */
        }

        .custom-bg {
            background-color: #dddbdb;

        }

        .bg-table1 {
            background-color: #ffffff;
        }
    </style>



    <script>



        const productPrices = {
            "ÜRE": 15,
            "%26 CAN": 9,
            "Amonyum Sülfat(Şeker Gübresi)": 7

        }
        let totalQuantity = 0;
        let totalPrice = 0;

        function selected() {
            let selectedProduct = document.getElementById("productSelect")
            let selectedValue = selectedProduct.options[selectedProduct.selectedIndex].text;

            console.log("seçilen ürün " + selectedValue);
        }

        function sil(row) {
            row.remove();
        }
        let sayi = document.getElementById("productQuantity")
        if (sayi % 50 === 0) {

        }
        i = 1;
        function ekle() {
            let sayi = parseInt(document.getElementById("productQuantity").value);

            if (sayi % 50 === 0) {



                let productName = document.getElementById("productSelect").options[document.getElementById("productSelect").selectedIndex].text;
                let fiyat = productPrices[productName] * 50;
                let toplam_fiyat = (fiyat / 50) * sayi;
                console.log(toplam_fiyat)

                totalQuantity += parseInt(sayi);
                totalPrice += toplam_fiyat;
                document.getElementById("totalQuantity").textContent = totalQuantity + " kg";
                document.getElementById("totalPrice").textContent = totalPrice + " TL";





                let tr = document.createElement("tr");

                if (i % 2 == 0) {
                    tr.style.backgroundColor = "lightgreen";
                }



                let td2 = document.createElement("td");
                td2.textContent = productName;
                tr.appendChild(td2);

                let td3 = document.createElement("td");
                td3.textContent = sayi + "kg";
                tr.appendChild(td3);

                let fiyatCell = document.createElement("td");
                fiyatCell.textContent = fiyat;
                tr.appendChild(fiyatCell);

                let td5 = document.createElement("td");
                td5.textContent = toplam_fiyat;
                tr.appendChild(td5);

                //butona style verme olayını hocaya sor
                let deleteButton = document.createElement("button");
                deleteButton.appendChild(document.createTextNode("Sil"));
                deleteButton.addEventListener("click",
                    function () {
                        let removedQuantity = parseInt(this.parentNode.childNodes[1].textContent);
                        let removedPrice = this.parentNode.childNodes[3].textContent;
                        sil(this.parentNode);
                        totalQuantity -= removedQuantity;
                        totalPrice -= removedPrice;
                        console.log(totalPrice);
                        console.log(toplam_fiyat);

                        document.getElementById("totalPrice").textContent = totalPrice
                        document.getElementById("totalQuantity").textContent = totalQuantity + " kg";


                    });
                tr.appendChild(deleteButton);



                let table = document.getElementById("orderList");
                table.appendChild(tr);


                alert("Ürün başarıyla eklendi")









                i++;
            }
            else {
                alert("Hatalı sayı girdiniz. Lütfen 50 ve 50'nin katı olacak şekilde giriniz!")
            }
            console.log(sayi);



        }
        function Goexcel() 
        {
            

        }

    </script>
</head>

<body class="bg-green " style="margin: 25px;">
    <form id="form1" runat="server">
    <div id="customerInfo">
        <table>

            <tr>
                <td>
                    İsim:
                </td>
                <td>
                    <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                    <br>
                </td>
            </tr>
            <tr>
                <td>
                    Soyisim:
                </td>
                <td>
                    <asp:TextBox ID="txtSurname" runat="server"></asp:TextBox>
                    <br>
                </td>
            </tr>

            <tr>
                <td>
                    TC Kimlik No:
                </td>
                <td>
                    <asp:TextBox ID="txtID" runat="server"></asp:TextBox>
                    <br>
                </td>
            </tr>
            <tr>
                <td>
                    Telefon Numarası:
                </td>
                <td>
                    <asp:TextBox ID="txtGSM" runat="server"></asp:TextBox>
                    <br>
                </td>
            </tr>
            <tr>
                <td>
                    E-mail:
                </td>
                <td>
                    <asp:TextBox ID="txtMail" runat="server"></asp:TextBox>
                    <br>
                </td>
            </tr>
            <tr>
                <td>
                    Adres:
                </td>
                <td>
                    <asp:TextBox ID="txtAdres" runat="server"></asp:TextBox>
                    <br>

                </td>
            </tr>

        </table>
        <br><br>
    </div>
    <div id="order">
        <table>
            <tr>
                <td><label for="productName">Ürün Seçiniz:</label>
                </td>
                <td>
                    <select class="custom-bg" name="productName" id="productSelect" onchange="selected()">
                        <option value="0">Seçiniz...</option>
                        <option value="1">ÜRE</option>
                        <option value="2">%26 CAN</option>
                        <option value=" 3">Amonyum Sülfat(Şeker Gübresi)</option>
                    </select>
                </td>
            </tr>
            <tr>
                <td><label for="productQuantity">Miktar(kg):</label></td>
                <td><input class="custom-bg" type="number" id="productQuantity"> <button onclick="ekle()">EKLE</button>
                </td>
            </tr>
        </table>
    </div>
    <div>
        <table class="bg-table1" style="border: solid; width: 750px;" id="orderList">
            <tr style="border-bottom: solid;">
                <th class="bg-table1">Ürün Adı</th>
                <th class="bg-table1">Miktar</th>
                <th class="bg-table1">Birim Fiyat</th>
                <th class="bg-table1"> Fiyat</th>
            </tr>
        </table>
        <div>
            <table class="custom-bg" style="border: solid; width: 750px;" id="totalOrder">
                <tr>
                    <th></th>
                    <th>Toplam miktar</th>
                    <th></th>
                    <th>Toplam Fiyat</th>
                </tr>
                <tr>
                    <td></td>
                    <td id="totalQuantity">0 kg</td>
                    <td></td>
                    <td id="totalPrice">0 TL</td>

                </tr>
            </table>
            <BR></BR>
            <asp:Button  ID="GoExcel" runat="server" Text="Excele Geçir" Width="154px" />
            <BR></BR>

            
        </div>

    </div>




    <!-- Bootstrap CSS dosyasını dahil etme -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet">

    <!-- Bootstrap JavaScript dosyasını dahil etme -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script>

    </form>

</body>

</html>