function sent() {
    let userInput = document.getElementById('Chat').value;
    document.getElementById('UserText').innerText = userInput;
    document.getElementById('Chat').value = "";



    if (userInput === "iyi" || userInput === "iyiyim" || userInput==="harika") {
        console.log("Harika! Size nasıl yardımcı olabilirim?");
        document.getElementById('BotAnswer').innerText = "Harika! Size sadece 4 işlem konusunda yardımcı olabilirim";

    }
    else if (userInput === "kötü" || userInput === "kötüyüm" || userInput === "berbat") {
        console.log("Umarım sana yardımcı olabilirim. Problem nedir?");
        document.getElementById('BotAnswer').innerText = "Üzüldüm fakat size sadece 4 işlem konusunda yardımcı olabilirim.";

    }
    

    
    else {
        // console.log("Anladım, size nasıl yardımcı olabilirim?");
        // document.getElementById('BotAnswer').innerText = "Demek istediğini tam olarak anlayamadım";
        let islem = userInput;
    try {
        let sonuc;
        if(userInput.includes('+')){
            sonuc= eval(userInput);
        }
        else if (userInput.includes('-')){
            sonuc = eval(userInput);
        }
        else if (userInput.includes('*')){
            sonuc = eval(userInput);
        }
        else if (userInput.includes('/')){
            sonuc = eval(userInput);
        }
        else{
            throw "Geçersiz işlem!!";
        }
        document.getElementById('BotAnswer').innerText = "Sonuç:" + sonuc;
    }
    catch (error) {
        document.getElementById('BotAnswer').innerText = "Geçersiz ifadeler kullandınız"

    }

    }




    /*prompt("Ne dediğini tam anlayamadım daha iyi anlatır mısın?")*/
}
function calculate() {
    let islem = document.getElementById('Chat').value;
    try {
        let sonuc = eval(islem);
        document.getElementById('BotAnswer'), innerText = "Sonuç:" + sonuc;
    }
    catch (error) {
        document.getElementById('BotAnswer').innerText = "Geçersiz ifadeler kullandınız"

    }
}