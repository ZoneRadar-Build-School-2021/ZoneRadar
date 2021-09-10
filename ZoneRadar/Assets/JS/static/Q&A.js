function $g(selectorrule) {
    //判斷是否為id selector
    if (selectorrule.includes('#')) {
        //回傳Element
        return document.querySelector(selectorrule);
    }
    //回傳NodeList集合
    var nodelist = document.querySelectorAll(selectorrule);
    return nodelist.length == 1 ? nodelist[0] :
        nodelist;
}

let select = $g('.QA-select');

select.querySelectorAll("option")[0].setAttribute("class", "d-none");
let btn1 = $g('#btn1');
let btn2 = $g('#btn2');
let btn3 = $g('#btn3');
let btn4 = $g('#btn4');
let btn5 = $g('#btn5');
let btn6 = $g('#btn6');
let btn7 = $g('#btn7');
let btn8 = $g('#btn8');
let btn9 = $g('#btn9');
let btn10 = $g('#btn10');
let QALsit = $g('.QA-List').querySelector('.col-10');


btn1.onclick = function () {
    QALsit.innerHTML = "";
    // containr.append().innerHTML="";
    let QA1 = $g('#QA-2');
    let cloneContent = QA1.content.cloneNode(true);
    // QA titel
    cloneContent.querySelector("h1").innerText = "說明服務";
    // QA
    cloneContent.querySelector("button").innerHTML = "<li>使用場地家訂場地有哪些好處？</li>";
    cloneContent.querySelector("p").innerHTML = "任何與活動場地有關的需求，都可以在ZoneRadar找到最佳解答：尋找合適場地、查看場地評價、線上場勘、預訂場地、代收款項、活動餐點、場地額外要求，通通都能在平台上一條龍輕鬆解決！<b>ZoneRadar場地家平台提供四大保證</b><br>1.場地多元豐富：豐富齊全的場地資訊、真實用戶評論、快速比較、精準搜尋<br>2.安心履約保障：第三方把關、多元付款、個資保證，合法商家、紛爭仲裁<br>3.價格精打細算：免手續費、買貴退差價、會員專屬優惠、限時免費取消<br>4.操作便利省時：步驟簡單、即時預訂、客服聯繫、訂單狀態隨時查</p>";
    cloneContent.querySelector("#flush-headingTwo").querySelector("button").innerHTML = "<li>ZoneRadar</li>";
    cloneContent.querySelector("#flush-collapseTwo").querySelector("p").innerHTML = "有感於各行各業豐富的會議需求卻缺乏便捷高效的媒合方式，因此，焦點會議科技用心創造出一個集中所有場地的預約平台。集結最豐富、齊全的場地選擇，讓場地精準地被搜尋與預訂。<br>ZoneRadar場地家，提供一個更完善的各式空間平台，我們看見您的困擾、傾聽您的需求、讓我們陪著您，化繁為簡！舉辦活動一切就是這麼輕鬆簡單！我們集結最豐富最齊全的場地選擇，挑好喜歡的場地後無須繁雜的手續，幾個步驟就能完成預約。<br>場地主可省時省力地在平台上刊登場地，快速的出租場地。ZoneRadar場地家，讓活動籌備更省時省力！讓活動籌組不孤單！ 加入場地家，你就是贏家！";
    QALsit.append(cloneContent);
}
btn2.onclick = function () {
    QALsit.innerHTML = "";
    // containr.append().innerHTML="";
    let QA2 = $g('#QA-10');
    let cloneContent = QA2.content.cloneNode(true);
    cloneContent.querySelector("h1").innerText = "註冊/會員資料";
    cloneContent.querySelector("button").innerHTML = "<li>為什麼會員電子報取消訂閱仍收到郵件？</li>";
    cloneContent.querySelector("p").innerHTML = "系統處理需要一定作業時間，取消後仍有可能會收到已排定寄出的促銷郵件。如取消超過二周後仍然持續收到促銷郵件，請再連繫平台客服人員確認";
    cloneContent.querySelector("#flush-headingTwo").querySelector("button").innerHTML = "<li>會員電子報如何訂閱/取消？</li>";
    cloneContent.querySelector("#flush-collapseTwo").querySelector("p").innerHTML = "登入平台後點選「會員資料管理」，在「是否想收到SpaceAdviosr場地家的促銷郵件」的選項中選擇「是/否」，確認後儲存即成功/取消訂閱。";
    cloneContent.querySelector("#flush-headingThree").querySelector("button").innerHTML = "<li>會員電子報</li>"
    cloneContent.querySelector("#flush-collapseThree").querySelector("p").innerHTML = "SpaceAdviosr場地家促銷郵件內容豐富，包含合作場地深入介紹、實用資訊文章介紹，並不定期釋出預約場地優惠資訊，只要訂閱就能掌握第一手的優惠";
    cloneContent.querySelector("#flush-headingFour").querySelector("button").innerHTML = "<li>修改密碼</li>";
    cloneContent.querySelector("#flush-collapseFour").querySelector("p").innerHTML = "登入平台後點選「會員資料」，依序填寫舊密碼、新密碼、再次確認新密碼，儲存即完成修改。";
    cloneContent.querySelector("#flush-headingFive").querySelector("button").innerHTML = "<li>忘記密碼</li>";
    cloneContent.querySelector("#flush-collapseFive").querySelector("p").innerHTML = "在首頁右上角點選「登入」圖示，根據跳窗指定上點選「忘記密碼」，輸入原始註冊e-mail，系統將寄發通知信來重新設定密碼。";
    cloneContent.querySelector("#flush-headingSix").querySelector("button").innerHTML = "<li>如何修改會員資料？</li>";
    cloneContent.querySelector("#flush-collapseSix").querySelector("p").innerHTML = "登入平台後點選「會員資料管理」，選擇並完成想要修改的項目，填寫完後儲存即完成修改。";
    cloneContent.querySelector("#flush-headingSeven").querySelector("button").innerHTML = "<li>手機認證？</li>";
    cloneContent.querySelector("#flush-collapseSeven").querySelector("p").innerHTML = "<b>為什麼需要手機認證？</b><br>為了確保場地訂購機制不會被濫用，同時也保障雙方的權益，須留下手機號碼並進行驗證。<br><b>如何重新認證手機？</b><br>登入平台後點選「會員資料管理」，在「行動電話」欄位旁，點選「修改並重新驗證」，系統即會再次寄送驗證碼</p>";
    cloneContent.querySelector("#flush-headingEight").querySelector("button").innerHTML = "<li>沒收到新帳號認證信？</li>"
    cloneContent.querySelector("#flush-collapseEight").querySelector("p").innerHTML = "請先確認垃圾信件夾當中是否有會員認證信，因部份免費信箱系統會把平台通知信歸類到垃圾信件夾當中。請設定永遠對此一網域 (ZoneRadar.com)的接收許可。<br>如沒收到會員認證信，請至會員註冊頁面點選最下方「沒有收到註冊信？」，輸入註冊的E-mail，點選「發送驗證信」按鈕，系統將會再次發送會員認證信。";
    cloneContent.querySelector("#flush-headingNine").querySelector("button").innerHTML = "<li>如何註冊新帳號？</li>";
    cloneContent.querySelector("#flush-collapseNine").querySelector("p").innerHTML = "在首頁右上角點選「登入」圖示，可以直接使用Facebook、Gmail、LINE帳號登入，或點選「註冊新帳號」創建SpaceAdviosr會員帳號。";
    cloneContent.querySelector("#flush-headingTen").innerHTML = "";
    QALsit.append(cloneContent);
}
btn3.onclick = function () {
    QALsit.innerHTML = "";
    let QA3 = $g('#QA-5');
    let cloneContent = QA3.content.cloneNode(true);
    cloneContent.querySelector("h1").innerHTML = "場地搜尋";
    cloneContent.querySelector("button").innerHTML = "<li>網站上的場地星級是依據什麼樣的標準制定？</li>";
    cloneContent.querySelector("p").innerHTML = "網頁上所標示的星級皆由該場地主根據其所在當地市場規範標準自行提供。ZoneRadar場地家不為任何住宿評定星級，且不承擔該住宿所自評的星級準確性的責任。";
    cloneContent.querySelector("#flush-headingTwo").querySelector("button").innerHTML = "<li>如何獲得停車場訊息？</li>";
    cloneContent.querySelector("#flush-collapseTwo").querySelector("p").innerHTML = "搜尋時，您可以在「設施」欄中選擇「停車」一項來輔助搜尋。場地主也會在主頁上「交通資訊」欄提供此類訊息。";

    cloneContent.querySelector("#flush-headingThree").querySelector("button").innerHTML = "<li>我如何得知場地主是否有某一設施或設備？</li>";
    cloneContent.querySelector("#flush-collapseThree").querySelector("p").innerHTML = "搜尋時，您可以在“設施”欄所列選項中選擇一項來輔助搜尋。場地主也會在主頁的“方案介紹”提供此類信息。<br>如沒有您想要的資訊，您可以與我們聯繫 <a href=#>service@ZoneRadar.com</a> 或 運用線上溝通系統直接發送訊息詢問場地主。</p>";


    cloneContent.querySelector("#flush-headingFour").querySelector("button").innerHTML = "<li>如何找到適合的場地？</li>";
    cloneContent.querySelector("#flush-collapseFour").querySelector("p").innerHTML = "直接在ZoneRadar首頁篩選所需要的場地條件：場地類別、活動性質、地區、人數、日期並輸入關鍵字，可進一步篩選出符合要求的場地。<br>或者也可以從首頁點選的主題專區，根據舉辦的活動類型找到最理想場地。";
    cloneContent.querySelector("#flush-collapseFive").innerHTML = "";
    QALsit.append(cloneContent);
}
btn4.onclick = function () {
    QALsit.innerHTML = "";
    // containr.append().innerHTML="";
    let QA4 = $g('#QA-19');
    let cloneContent = QA4.content.cloneNode(true);
    cloneContent.querySelector("h1").innerHTML = "註冊/會員資料";
    cloneContent.querySelector("button").innerHTML = "<li>場地使用注意事項</li>";
    cloneContent.querySelector("p").innerHTML = "場地中的空間、設備設施等均屬於租用性質，敬請妥善使用、維護、保管。<br>如有任何物品損壞，請依照各場地主相關損壞賠償規定辦理。<br>各場地主的損害賠償規則不限於平台上公告之內容，如有疑義請逕洽各場地主諮詢。";
    cloneContent.querySelector("#flush-headingTwo").querySelector("button").innerHTML = "<li>什麼是「現場付款」？</li>";
    cloneContent.querySelector("#flush-collapseTwo").querySelector("p").innerHTML = "您訂購場地時，可以選擇場地使用當日再前往付款即可。亦將由場地主現場開立發票給您。<b>提醒您：請注意場地主現場可接受的收費方式。";
    cloneContent.querySelector("#flush-headingThree").querySelector("button").innerHTML = "<li>我要如何訂購多個場地？</li>";
    cloneContent.querySelector("#flush-collapseThree").innerHTML = "您可以將希望預定的場地加入購物車，從購物車中一起送出向場地主申請或是一起購買。";
    cloneContent.querySelector("#flush-headingFour").querySelector("button").innerHTML = "<li>我已在ZoneRadar場地家網站預訂場地，但是我發現了更便宜的價格</li>";
    cloneContent.querySelector("#flush-collapseFour").querySelector("p").innerHTML = "ZoneRadar場地家為您提供以下「最低價保證」。如果您已經透過ZoneRadar預訂場地，並在訂購後向我們證明您的確在其它網站上或是其他管道看到了相同日期、相同房型、相同條件下的更低預訂價格，在此情況下，我們將為您提供相同價格甚至更低價格。";
    cloneContent.querySelector("#flush-headingFive").querySelector("button").innerHTML = "<li>我是否可以向場地主直接進行預訂？</li>";
    cloneContent.querySelector("#flush-collapseFive").querySelector("p").innerHTML = "當然可以。但提醒您，ZoneRadar網站所提供的優惠價格，以及網站提供的積點服務只有透過我們預訂服務才有效。ZoneRadar積點將不限定場地，您所有的訂購都可以進行累積並享有回饋。<br>並且我們以第三方立場保障了您的訂購與使用權利，若有消費糾紛我們將協助您進行協商。";
    cloneContent.querySelector("#flush-headingSix").querySelector("button").innerHTML = "<li>訂購完成後可否變更訂單？</li>";
    cloneContent.querySelector("#flush-collapseSix").querySelector("p").innerHTML = "計畫還會變動？沒關係，您可以隨時變更訂單！您可隨時變更日期、時間、數量與場地。我們為您設計訂單隨時彈性調整的機制，讓您輕鬆訂購無負擔！";
    cloneContent.querySelector("#flush-headingSeven").querySelector("button").innerHTML = "<li>訂購完成後可否取消訂單？</li>";
    cloneContent.querySelector("#flush-collapseSeven").querySelector("p").innerHTML = "計畫還會變動？沒關係，您可以隨時取消訂單！付款後您享有的免費取消期限將依照場地主制定的期限。";
    cloneContent.querySelector("#flush-headingEight").querySelector("button").innerHTML = "<li>發出需求後如何更改需求？</li>";
    cloneContent.querySelector("#flush-collapseEight").innerHTML = "發出需求後若需更改，您可於訂單成立後進行變更，或是重新發送需求。";
    cloneContent.querySelector("#flush-headingNine").querySelector("button").innerHTML = "<li>發出需求後須等多久回覆？</li>";
    cloneContent.querySelector("#flush-collapseNine").querySelector("p").innerHTML = "如場地不可被線上即時訂購，則須發出需求等後場地主回覆確認訂單。<br>場地主通常於1-2個工作天內回覆您的訂單是否成立，如遇旺季或是訂單較多時，可能會延遲場地主回覆效率，還請您耐心等候！ 如您有較急的訂購需求或是已等候多時，歡迎您與場地家客服聯繫：<a href=mailto:service@ZoneRadarcom>service@ZoneRadar.com。</a>，我們將會盡快為您通知場地主。";
    cloneContent.querySelector("#flush-headingTen").querySelector("button").innerHTML = "<li>如何聯繫場地主？</li>";
    cloneContent.querySelector("#flush-collapseTen").querySelector("p").innerHTML = "訂單成立後，您可針對每一筆訂單分別與場地主聯繫訂單安排細節。相關的訊息紀錄會被記錄於系統中，保障您的權益。 登入會員後在訂單管理頁面找到指定訂單，點選「聯繫場地主」，即可與場地主聯繫。 訂單成立前無法提出詢問，如有任何問題歡迎與我們客服聯繫: <a href=mailto:service@ZoneRadar.com>service@ZoneRadar.com</a>";
    cloneContent.querySelector("#flush-headingEleven").querySelector("button").innerHTML = "<li>如何了解是否提供折扣優惠？</li>";
    cloneContent.querySelector("#flush-collapseEleven").querySelector("p").innerHTML = "所有的促銷價均為折後價格。如您有額外的折價券或折扣代碼，請於購買時輸入，金額將自動為您折抵。"
    cloneContent.querySelector("#flush-headingTwelve").querySelector("button").innerHTML = "<li>稅費和服務費金額是多少？</li>";
    cloneContent.querySelector("#flush-collapseTwelve").querySelector("p").innerHTML = "為方便您的交易，在您付費時，稅費和服務費會包括在內。各地的稅費由場地主自行定義，您可以在費用的後方看到稅費與服務費比例。";
    cloneContent.querySelector("#flush-headingThirteen").querySelector("button").innerHTML = "<li>價格是否包含稅費和服務費？</li>";
    cloneContent.querySelector("#flush-collapseThirteen").querySelector("p").innerHTML = "訂單明細的價格即為您所需支付的價格，價格已包含了所有額外費用。包含的額外費用將註明於價格後方，例如 NT$ Ten00 (含5%稅、Ten%服務費、清潔費...)";
    cloneContent.querySelector("#flush-headingFourteen").querySelector("button").innerHTML = "<li>我可否預訂未列出的價格方案？</li>";
    cloneContent.querySelector("#flush-collapseFourteen").querySelector("p").innerHTML = "若沒有找到您想要的價格方案 或 計費方式，可能是場地主尚未上架，您可以留下訊息，我們收到後將會為您確認並請場地主盡快上架，完成後將主動通知您前往訂購。";
    cloneContent.querySelector("#flush-headingFifteen").querySelector("button").innerHTML = "<li>為什麼同一場地有多種價格？</li>";
    cloneContent.querySelector("#flush-collapseFifteen").querySelector("p").innerHTML = "場地可以包裝許多套裝方案，每一個方案有不同的適用情境，也有不同的贈送內容。您可以選擇最優惠的方案訂購。提醒您：有部分的套裝有限定了使用身分，訂購前請注意方案說明，例如 謝師宴 (僅限學生身分訂購與使用)。";
    cloneContent.querySelector("#flush-headingSixteen").querySelector("button").innerHTML = "<li>價格是按人數計費、桌數計費還是按時間計？</li>";
    cloneContent.querySelector("#flush-collapseSixteen").querySelector("p").innerHTML = "請注意方案說明，有以人桌計費的方案 也有以時間計費的方案，您也可以篩選您想找的計費方式。";
    cloneContent.querySelector("#flush-headingSeventeen").querySelector("button").innerHTML = "<li>什麼是「即時預訂場地」？</li>";
    cloneContent.querySelector("#flush-collapseSeventeen").querySelector("p").innerHTML = "如果場地有標記閃電符號即為「即時預訂」，表示該場地不需要經過場地主核可，可供會員直接預訂，省略審核時間，讓預訂過程更省時。";
    cloneContent.querySelector("#flush-headingEighteen").querySelector("button").innerHTML = "<li>如何預訂場地？</li>";
    cloneContent.querySelector("#flush-collapseEighteen").querySelector("p").innerHTML = "<b>1. 提出申請</b><br>在場地頁面選擇訂購日期後，依照指示填寫時間及其他需求，點選「提出申請」按鈕，再輸入活動名稱及簡述，並輸入手機號碼，無誤後點選「發送驗證碼」，您的手機將會收到一組驗證碼，驗證碼輸入完成後，即完成「提出申請」的步驟，場地主通常在24小時內回覆是否可供租借。</p><p class=mb-0 text-secondary><b>2. 查看訂單</b><br>發出場地申請需求後，該筆訂單可在「我的訂單」頁面中查看，系統將會顯示審核中，不代表預訂成立，且暫時不收費。</p><p class=mb-0 text-secondary><b>3. 付款</b><br>如果場地主同意租借，系統將會再發訊息通知，可在「我的訂單」頁面中找到該筆訂單，在付款期限以前，點選「立即付款」後，按指示填寫資料，待付款成功後，才能完成場地的預訂。請注意，發出預訂請求不保證預訂成功，需待場地回覆確認，訂單才算成立。如訂單成立後，敬請於付款期限前完成付款，若逾期付款，系統將會自動取消訂單。";

    cloneContent.querySelector("#flush-headingNineteen").innerHTML = "";


    QALsit.append(cloneContent);
}
btn5.onclick = function () {
    QALsit.innerHTML = "";
    let QA5 = $g("#QA-10");
    let cloneContent = QA5.content.cloneNode("true");
    cloneContent.querySelector("h1").innerHTML = "付款"
    cloneContent.querySelector("button").innerHTML = "<li>私下付款風險</li>";
    cloneContent.querySelector("p").innerHTML = "為保障您的交易安全，除了Space Advisor網站及應用程式外，請勿使用其他任何方式轉帳或與場地主連繫。<br>建議您全程使用ZoneRadar溝通。<br>若平台上的場地主要求您透過ZoneRadar網站及應用程式以外的方式付款（包含但不限於匯款單、銀行本票、速匯金、自由儲備、西聯匯款等），請向平台客服舉報，並且拒絕匯款，以免遭遇詐騙。";
    cloneContent.querySelector("#flush-headingTwo").querySelector("button").innerHTML = "<li>輸入信用卡的詳細訊息是否安全？</li>";
    cloneContent.querySelector("#flush-collapseTwo").querySelector("p").innerHTML = "ZoneRadar採用安全套接層 (SSL) 技術加密所有訊息，對您的資訊安全嚴密保護，請放心訂購。";
    cloneContent.querySelector("#flush-headingThree").querySelector("button").innerHTML = "<li>付款保證</li>";
    cloneContent.querySelector("#flush-collapseThree").querySelector("p").innerHTML = "訂購場地成功後，付款前都享有免費取消。<br>完成付款後則依據場地主的取消規則中的免費取消期限。";
    cloneContent.querySelector("#flush-headingFour").querySelector("button").innerHTML = "<li>超過期限未付款</li>";
    cloneContent.querySelector("#flush-collapseFour").querySelector("p").innerHTML = "超過付款期限仍未付款的訂單，系統將會自動取消訂單，請重新訂購。";
    cloneContent.querySelector("#flush-headingFive").querySelector("button").innerHTML = "<li>確認付款成功</li>";
    cloneContent.querySelector("#flush-collapseFive").querySelector("p").innerHTML = "付款成功之後，系統將會自動寄送信件到您在平台上登記的E-mail信箱，通知您已付款成功。同時也可在訂單管理系統點選「訂單資訊」查看付款狀態。";
    cloneContent.querySelector("#flush-headingSix").querySelector("button").innerHTML = "<li>付款期限？</li>";
    cloneContent.querySelector("#flush-collapseSix").querySelector("p").innerHTML = "場地家為確保消費者權益，平台保障付款期限的規則如下：<br>・如您的活動日為下訂後30天以上(含)，付款期限為訂單確認後，第7天內完成付款<br>・如您的活動日為下訂後29天內(含)，付款期限為訂單確認後，第3天內完成付款<br>・如您的活動日為下訂後2-3天內，付款期限為活動日前一天的17:00前，須完成付款<br>・如您的活動日為1天內，當天須完成付款<br<br>※每筆訂單付款期限，依據每個場地主所訂定的規則而有所不同，如果是需要等候場地主確認的訂單，您需等候場地主接受訂單後才可付款，每筆訂單成立後將有不同的付款日期，取決於場地主當日的訂購狀況。<br>如果是可立即確認的訂單，您則於訂購完成後就須完成付款以利場地保留，但您仍可於訂單成立後進行修改或取消。";
    cloneContent.querySelector("#flush-headingSeven").querySelector("button").innerHTML = "<li>稅費和服務費金額是多少？</li>";
    cloneContent.querySelector("#flush-collapseSeven").querySelector("p").innerHTML = "為方便您的交易，在您使用信用卡結算時，稅費和服務費會包括在內。稅費 與 服務費比例將依照各場地主自行設定的比例。";
    cloneContent.querySelector("#flush-headingEight").querySelector("button").innerHTML = "<li>如果我用來確定預訂的信用卡無效或被取消，我該怎麽辦？</li>";
    cloneContent.querySelector("#flush-collapseEight").querySelector("p").innerHTML = "如果您的信用卡無效或被取消，請聯絡我們以免您的訂單因扣款失敗而自動取消。";
    cloneContent.querySelector("#flush-headingNine").querySelector("button").innerHTML = "<li>有哪些付款方式？</li>";
    cloneContent.querySelector("#flush-collapseNine").querySelector("p").innerHTML = "<b>信用卡</b> 全額付清、信用卡分期、先付訂金</p><p class=mb-0 text-secondary><b>Visa金融卡(Debit卡)或銀行ATM轉帳</b> 消費前需先確認帳戶內有餘額才能使用，且金融卡無法使用銀行分期付款。如使用金融卡，請於扣款日前確保您的餘額充足，以防訂單因扣款失敗而被取消。</p><p class=mb-0 text-secondary><b>現場付款</b><br>若您選擇「現場付款」，需輸入您的卡號做為保證及存留用途，若當日未至現場完成付款，將從信用卡中扣款。 提醒您訂單總金額超過5萬元不提供現場付款的選項。";
    cloneContent.querySelector("#flush-headingTen").querySelector("button").innerHTML = "<li>預定場地需要支付哪些款項？</li>";
    cloneContent.querySelector("#flush-collapseTen").querySelector("p").innerHTML = "透過ZoneRadar搜尋場地、篩選場地、預訂場地，您不需要支付任何平台手續費，永久免手續費。訂單成立後才需支付您所訂購的場地相關費用。";
    QALsit.append(cloneContent);
}
btn6.onclick = function () {
    QALsit.innerHTML = "";
    let QA6 = $g("#QA-19");
    let cloneContent = QA6.content.cloneNode(true);
    cloneContent.querySelector("h1").innerHTML = "訂單查詢/修改";
    cloneContent.querySelector("button").innerHTML = "<li>發票資訊錯誤</li>";
    cloneContent.querySelector("p").innerHTML = "如發生發票資訊錯誤的情況，請洽詢ZoneRadar客服 (service@ZoneRadar.com)，我們將有專人為您服務。";
    cloneContent.querySelector("#flush-headingTwo").querySelector("button").innerHTML = "<li>電子發票</li>";
    cloneContent.querySelector("#flush-collapseTwo").querySelector("p").innerHTML = "會員載具，若中獎將寄送至您所輸入的地址。";
    cloneContent.querySelector("#flush-headingThree").querySelector("button").innerHTML = "<li>三聯式發票</li>";
    cloneContent.querySelector("#flush-collapseThree").querySelector("p").innerHTML = "在訂購流程中填寫訂購資料時，可以直接選擇「三聯式發票」，再填寫發票抬頭與統一編號。平台依據財政部推行發票無紙化，採電子發票的形式進行發票作業，其效力與紙本發票相同，電子憑證也將寄送至您所登記的電子信箱。";
    cloneContent.querySelector("#flush-headingFour").querySelector("button").innerHTML = "<li>如何開立發票包含我公司名稱與統一編號？</li>";
    cloneContent.querySelector("#flush-collapseFour").querySelector("p").innerHTML = "請於付款時選擇三聯式發票，並填寫您的公司名稱與統一編號。";
    cloneContent.querySelector("#flush-headingFive").querySelector("button").innerHTML = "<li>如何查看發票？</li>";
    cloneContent.querySelector("#flush-collapseFive").querySelector("p").innerHTML = "您可至我的訂單中，查詢每筆訂單的發票。";
    cloneContent.querySelector("#flush-headingSix").querySelector("button").innerHTML = "<li>能否郵寄紙本收據？</li>";
    cloneContent.querySelector("#flush-collapseSix").querySelector("p").innerHTML = "ZoneRadar全面使用電子發票，無實體發票。我們會主動協助兌獎，並於中獎時通知兌獎事宜。如您選擇開立三聯式發票，我們將mail憑證給您，請注意您的電子郵件信箱。";
    cloneContent.querySelector("#flush-headingSeven").querySelector("button").innerHTML = "<li>私訊功能</li>";
    cloneContent.querySelector("#flush-collapseSeven").querySelector("p").innerHTML = "▲ 查看/回覆訊息<br>登入會員後，在首頁右上角點選信件夾圖示，即可進入收件夾查看與回覆系統通知和站內訊息。<br>請注意，訂單成立後才可進行安排內容的詳細溝通";
    cloneContent.querySelector("#flush-headingEight").querySelector("button").innerHTML = "<li>當日使用場地時要向場地主出示什麽？</li>";
    cloneContent.querySelector("#flush-collapseEight").querySelector("p").innerHTML = "依據每個場地主的規則不同，一般而言您需要出示：<br>- ZoneRadar場地家訂購憑證或訂單編號<br>- 預訂時所用的信用卡<br>- 有效身份證件";
    cloneContent.querySelector("#flush-headingNine").querySelector("button").innerHTML = "<li>修改訂單</li>";
    cloneContent.querySelector("#flush-collapseNine").querySelector("p").innerHTML = "成功預訂場地後，可以再進行人數/桌數、時間等安排的修改。<br>您可至「我的訂單」找到訂單，並進行需求變更。<br>提醒您，減少的部份視同取消，取消金比照取消條款辦理。";
    cloneContent.querySelector("#flush-headingTen").querySelector("button").innerHTML = "<li>如何得知我的預訂已被確認？</li>";
    cloneContent.querySelector("#flush-collapseTen").querySelector("p").innerHTML = "您會在預訂成功後 3 分鐘內收到該電子郵件。如果您未收到訂購確認通知，請檢查您的垃圾郵件和/或垃圾郵件過濾器。您也可以透過網站登入「我的預訂」頁面來檢查您的預訂狀態。";
    cloneContent.querySelector("#flush-headingEleven").querySelector("button").innerHTML = "<li>為什麽場地主無預訂記錄？</li>";
    cloneContent.querySelector("#flush-collapseEleven").querySelector("p").innerHTML = "所有預訂都會立即確認。如果場地主沒有您的預訂記錄，請立即聯絡ZoneRadar。";
    cloneContent.querySelector("#flush-headingTwelve").querySelector("button").innerHTML = "<li>如何更換場地主或方案？</li>";
    cloneContent.querySelector("#flush-collapseTwelve").querySelector("p").innerHTML = "更換場地主/方案需要取消您的原始預訂並重新預訂。請注意，取消任何預訂均須依據場地主的取消預訂政策進行。";
    cloneContent.querySelector("#flush-headingThirteen").querySelector("button").innerHTML = "<li>可否加購餐飲、住宿或設備？例如投影機或麥克風？</li>";
    cloneContent.querySelector("#flush-collapseThirteen").querySelector("p").innerHTML = "在場地主有販售的情況下，您可以於訂購當下 或是訂購完成後，選擇加購餐飲、住宿或設備。";
    cloneContent.querySelector("#flush-headingFourteen").querySelector("button").innerHTML = "<li>查詢訂單狀態</li>";
    cloneContent.querySelector("#flush-collapseFourteen").querySelector("p").innerHTML = "進入<b>「我的訂單」</b>頁面，會顯示五種不同的訂單狀態：</p><p class=mb-0 text-secondary><br>1.審核中 若訂單尚未經過場地主審核，訂單將出現在「審核中」分頁。<p class=mb-0 text-secondary><br>2.待付款 若訂單已經過場地主審核，但尚未完成付款，訂單將出現在「待付款」分頁</p><p class=mb-0 text-secondary><br>3.已付款 若訂單已完成付款，訂單將出現在「已付款」分頁</p><p class=mb-0 text-secondary><br>4.已結束 若訂單的活動日期已結束，訂單將出現在「已結束」分頁</p><p class=mb-0 text-secondary><br>5.已取消 若訂單已取消，訂單將出現在「已取消」的分頁。</p><p class=mb-0 text-secondary><br>▲ 若想查看每筆訂單的詳情，點選「明細管理」，則可看到訂單的詳細資訊。";
    cloneContent.querySelector("#flush-headingFifteen").querySelector("button").innerHTML = "<li>發票何時會開立？</li>";
    cloneContent.querySelector("#flush-collapseFifteen").querySelector("p").innerHTML = "發票將統一於活動結束後一日開立。";
    cloneContent.querySelector("#flush-headingSixteen").querySelector("button").innerHTML = "";
    cloneContent.querySelector("#flush-headingSeventeen").querySelector("button").innerHTML = "";
    cloneContent.querySelector("#flush-headingEighteen").querySelector("button").innerHTML = "";
    cloneContent.querySelector("#flush-headingNineteen").querySelector("button").innerHTML = "";
    QALsit.append(cloneContent);

}
btn7.onclick = function () {
    QALsit.innerHTML = "";
    // containr.append().innerHTML="";
    let QA7 = $g('#QA-2');
    let cloneContent = QA7.content.cloneNode(true);
    // QA titel
    cloneContent.querySelector("h1").innerText = "取消預定";
    // QA
    cloneContent.querySelector("button").innerHTML = "<li>取消訂單</li>";
    cloneContent.querySelector("p").innerHTML = "ZoneRadar為您提供自助服務選項。您可以透過網站中的「我的預訂」頁面進行取消預訂。系統將為您試算應退款或產生的費用。</p><p><br><b>怎樣正確計算取消預訂日期？</b><br>取消預訂政策中所表示的取消天數規範須以您預訂場地的當地日曆天為計算標準，即表示如果過了當地時間午夜12點則須以隔天計算。</p><p><br><b>取消訂單後，折扣代碼仍可使用嗎？</b><br>取消預訂時，當筆訂單將不可再使用折扣代碼，亦不再享有折扣的優惠金額。 若您取消訂單，原折扣代碼將退還至您的帳戶中，您可於折扣代碼使用期限內使用至其他訂單中。</p><p><br><b>取消費用計算</b><br>多數訂單都包含免費取消的期限。根據場地使用時段、場地類型、促銷活動等等，每次預訂的取消政策的細節和條款均有所不同。您可以於訂單明細中查看您可免費取消的期限，如已過免費取消期限則需依據場地主的取消政策支付取消費用。</p><p><br><b>取消需要再付款的情況</b><br>若您訂單預訂時有使用折扣代碼，如欲取消將先扣除折扣代碼的優惠金額，再進行取消金額計算，若可退款的金額小於折扣代碼金額，就需要再另行支付差額。</p><p><br><b>取消成功後的退款方式</b><br>如您的訂單是使用信用卡付款，當您的訂單成功取消後，應退款項將於次一個結帳日退回至您的信用卡帳戶中。<br>如您是使用ATM付款，當您的訂單成功取消後，請與客服聯繫並告知您的退款帳號，請注意，系統僅接受與訂購者本人相同名稱之銀行帳戶。<br>如付款日與取消日間隔一年以上的訂單，由於金流作業的限制，如欲取消，請與客服聯繫。<br>若有其他問題請洽ZoneRadar場地家客服人員。</p>";
    cloneContent.querySelector("#flush-headingTwo").querySelector("button").innerHTML = "<li>免費取消期限說明</li>";
    cloneContent.querySelector("#flush-collapseTwo").querySelector("p").innerText = "如您訂購一個月以後的場地，則可享有平台保障7天內免費取消的權益，若超過7天以上欲取消場地，則依照場地主訂定的取消金比例計算。<br>注意：取消金額與取消日相關，如欲取消建議您盡早取消以降低取消金。<br>如您訂購一個月以內的場地，如付款後欲取消，則依場地主訂定的取消比例計算之。<br>場地主的取消比例皆按照距離活動日的天數收取不同比例的手續費，而每間場地的退款規定皆不相同，(詳細內容請參考場地頁中的取消條款說明)";
    QALsit.append(cloneContent);
}
btn8.onclick = function () {
    QALsit.innerHTML = "";
    let QA8 = $g("#QA-5");
    let cloneContent = QA8.content.cloneNode(true);
    cloneContent.querySelector("h1").innerHTML = "場地評論"
    cloneContent.querySelector("button").innerHTML = "<li>如何分享場地評論？</li>";
    cloneContent.querySelector("p").innerHTML = "您可以在場地頁面下方點選「評價」撰寫您對場地的心得，或者在訂單頁面點選「前往評價」，會員每對一個場地進行評論，即可獲得1點評論點數。<br>會員帳號每日最多只能評論3個場地，且每日可獲得的評論點數上限為3點。<br>詳細說明請至活動說明頁面查詢。 <a href=https://www.ZonRadar.com/>https://www.ZonRadar.com/</a> ";
    cloneContent.querySelector("#flush-headingTwo").querySelector("button").innerHTML = "<li>如何檢舉評論？</li>";
    cloneContent.querySelector("#flush-collapseTwo").querySelector("p").innerHTML = "您可以在場地頁面下方點選「評價」撰寫您對場地的心得，或者在訂單頁面點選「前往評價」，會員每對一個場地進行評論，即可獲得1點評論點數。<br>會員帳號每日最多只能評論3個場地，且每日可獲得的評論點數上限為3點。<br>詳細說明請至活動說明頁面查詢。 <a href=https://www.ZonRadar.com/ >https://www.ZonRadar.com/ </a>";
    cloneContent.querySelector("#flush-headingThree").querySelector("button").innerHTML = "<li>評論注意事項</li>";
    cloneContent.querySelector("#flush-collapseThree").querySelector("p").innerHTML = "請填寫符合您真實經歷的場地評論，並評價星等，評論字數至少需50字以上，提醒您評論內容不得涉及不雅用語、人身攻擊、敏感題材或其他不適當內容。<br>請不要包含地址和電話號碼等個人信息。<br>違反審查規範或我們網站的服務條款以及其他政策的評論可能會被拒絕發布或刪除。評論前請查閱評論聲明注意事項 <a href=https://www.ZonRadar.com/reward>https://www.ZonRadar.com/reward</a><br><br>您的評論發布後，將經過3-5個工作天審查，一經審查通過將自動公開發佈。評論一經發布將不可刪除。";
    cloneContent.querySelector("#flush-headingFour").querySelector("button").innerHTML = "<li>場地評論的好處</li>";
    cloneContent.querySelector("#flush-collapseFour").querySelector("p").innerHTML = "透過您分享的評論，可以讓更多場地租用者事先了解場地狀況。同樣您也可以參考其他網友在場地留下的評論，找到理想中的場地喔！";
    cloneContent.querySelector("#flush-headingFive").innerHTML = "";

    QALsit.append(cloneContent);
}
btn9.onclick = function () {
    QALsit.innerHTML = "";
    let QA9 = $g("#QA-5");
    let cloneContent = QA9.content.cloneNode(true);
    cloneContent.querySelector("h1").innerHTML = "取消預定";
    cloneContent.querySelector("button").innerHTML = "<li>取消訂單</li>";
    cloneContent.querySelector("p").innerHTML = "SpaceAdvisor為您提供自助服務選項。您可以透過網站中的「我的預訂」頁面進行取消預訂。系統將為您試算應退款或產生的費用。</p><p class=mb-0 text-secondary><br><b>怎樣正確計算取消預訂日期？</b><br>取消預訂政策中所表示的取消天數規範須以您預訂場地的當地日曆天為計算標準，即表示如果過了當地時間午夜12點則須以隔天計算。</p><p class=mb-0 text-secondary><br><b>取消訂單後，折扣代碼仍可使用嗎？</b><br>取消預訂時，當筆訂單將不可再使用折扣代碼，亦不再享有折扣的優惠金額。 若您取消訂單，原折扣代碼將退還至您的帳戶中，您可於折扣代碼使用期限內使用至其他訂單中。</p><p class=mb-0 text-secondary><br><b>取消費用計算</b><br>多數訂單都包含免費取消的期限。根據場地使用時段、場地類型、促銷活動等等，每次預訂的取消政策的細節和條款均有所不同。您可以於訂單明細中查看您可免費取消的期限，如已過免費取消期限則需依據場地主的取消政策支付取消費用。</p><p class=mb-0 text-secondary><br><b>取消需要再付款的情況</b><br>若您訂單預訂時有使用折扣代碼，如欲取消將先扣除折扣代碼的優惠金額，再進行取消金額計算，若可退款的金額小於折扣代碼金額，就需要再另行支付差額。";
    cloneContent.querySelector("#flush-headingTwo").querySelector("button").innerHTML = "<li>異業合作聯絡管道</li>";
    cloneContent.querySelector("#flush-collapseTwo").querySelector("p").innerHTML = "請洽客服中心：<a href=mailto:service@ZonRadar.com?subject=【異業合作聯絡】>service@ZonRadar.com</a>";
    cloneContent.querySelector("#flush-headingThree").querySelector("button").innerHTML = "<li>場地業者聯絡管道</li>";
    cloneContent.querySelector("#flush-collapseThree").querySelector("p").innerHTML = "如欲申請成為場地主，請先填寫場地主申請資料：<a href=http://support@ZonRadar.com>https://www.ZonRadar.com/space/readme</a></p><p class=mb-0 text-secondary>如欲詢問其他事宜， 請洽場地主客戶服務中心：<a href=mailto:support@ZonRadar.com?subject=【場地業者聯絡管道】>support@ZonRadar.com</a>";
    cloneContent.querySelector("#flush-headingFour").querySelector("button").innerHTML = "<li>一般會員聯絡管道</li>";
    cloneContent.querySelector("#flush-collapseFour").querySelector("p").innerHTML = ">請洽客服中心：<a href=mailto:service@ZonRadar.com>service@ZonRadar.com</a>";

    cloneContent.querySelector("#flush-headingFive").innerHTML = "";
    QALsit.append(cloneContent);
}
btn10.onclick = function () {
    QALsit.innerHTML = "";
    let QA10 = $g('#QA-2')
    let cloneContent = QA10.content.cloneNode(true);
    cloneContent.querySelector("h1").innerText = "場地資訊錯誤回報";
    cloneContent.querySelector("button").innerHTML = "<li>場地資訊錯誤回報</li>";
    cloneContent.querySelector("p").innerHTML = "請洽客服中心：<a href=mailto:service@ZoneRadar.com?subject=【場地資訊錯誤回報】>service@ZoneRadar.com</a>";
    cloneContent.querySelector("#flush-headingTwo").innerHTML = "";
    QALsit.append(cloneContent);
}

let QASelect = $g('.QA-select');
QASelect.addEventListener('change', function () {
    if (this.value == 1) {
        QALsit.innerHTML = "";
        // containr.append().innerHTML="";
        let QA1 = $g('#QA-2');
        let cloneContent = QA1.content.cloneNode(true);
        // QA titel
        cloneContent.querySelector("h1").innerText = "說明服務";
        // QA
        cloneContent.querySelector("button").innerHTML = "<li>使用場地家訂場地有哪些好處？</li>";
        cloneContent.querySelector("p").innerHTML = "任何與活動場地有關的需求，都可以在ZoneRadar找到最佳解答：尋找合適場地、查看場地評價、線上場勘、預訂場地、代收款項、活動餐點、場地額外要求，通通都能在平台上一條龍輕鬆解決！<b>ZoneRadar場地家平台提供四大保證</b><br>1.場地多元豐富：豐富齊全的場地資訊、真實用戶評論、快速比較、精準搜尋<br>2.安心履約保障：第三方把關、多元付款、個資保證，合法商家、紛爭仲裁<br>3.價格精打細算：免手續費、買貴退差價、會員專屬優惠、限時免費取消<br>4.操作便利省時：步驟簡單、即時預訂、客服聯繫、訂單狀態隨時查</p>";
        cloneContent.querySelector("#flush-headingTwo").querySelector("button").innerHTML = "<li>ZoneRadar</li>";
        cloneContent.querySelector("#flush-collapseTwo").querySelector("p").innerText = "有感於各行各業豐富的會議需求卻缺乏便捷高效的媒合方式，因此，焦點會議科技用心創造出一個集中所有場地的預約平台。集結最豐富、齊全的場地選擇，讓場地精準地被搜尋與預訂。<br>ZoneRadar場地家，提供一個更完善的各式空間平台，我們看見您的困擾、傾聽您的需求、讓我們陪著您，化繁為簡！舉辦活動一切就是這麼輕鬆簡單！我們集結最豐富最齊全的場地選擇，挑好喜歡的場地後無須繁雜的手續，幾個步驟就能完成預約。<br>場地主可省時省力地在平台上刊登場地，快速的出租場地。ZoneRadar場地家，讓活動籌備更省時省力！讓活動籌組不孤單！ 加入場地家，你就是贏家！";
        QALsit.append(cloneContent);
    }
    if (this.value == 2) {
        QALsit.innerHTML = "";
        // containr.append().innerHTML="";
        let QA2 = $g('#QA-10');
        let cloneContent = QA2.content.cloneNode(true);
        cloneContent.querySelector("h1").innerText = "註冊/會員資料";
        cloneContent.querySelector("button").innerHTML = "<li>為什麼會員電子報取消訂閱仍收到郵件？</li>";
        cloneContent.querySelector("p").innerHTML = "系統處理需要一定作業時間，取消後仍有可能會收到已排定寄出的促銷郵件。如取消超過二周後仍然持續收到促銷郵件，請再連繫平台客服人員確認";
        cloneContent.querySelector("#flush-headingTwo").querySelector("button").innerHTML = "<li>會員電子報如何訂閱/取消？</li>";
        cloneContent.querySelector("#flush-collapseTwo").querySelector("p").innerHTML = "登入平台後點選「會員資料管理」，在「是否想收到SpaceAdviosr場地家的促銷郵件」的選項中選擇「是/否」，確認後儲存即成功/取消訂閱。";
        cloneContent.querySelector("#flush-headingThree").querySelector("button").innerHTML = "<li>會員電子報</li>"
        cloneContent.querySelector("#flush-collapseThree").querySelector("p").innerHTML = "SpaceAdviosr場地家促銷郵件內容豐富，包含合作場地深入介紹、實用資訊文章介紹，並不定期釋出預約場地優惠資訊，只要訂閱就能掌握第一手的優惠";
        cloneContent.querySelector("#flush-headingFour").querySelector("button").innerHTML = "<li>修改密碼</li>";
        cloneContent.querySelector("#flush-collapseFour").querySelector("p").innerHTML = "登入平台後點選「會員資料」，依序填寫舊密碼、新密碼、再次確認新密碼，儲存即完成修改。";
        cloneContent.querySelector("#flush-headingFive").querySelector("button").innerHTML = "<li>忘記密碼</li>";
        cloneContent.querySelector("#flush-collapseFive").querySelector("p").innerHTML = "在首頁右上角點選「登入」圖示，根據跳窗指定上點選「忘記密碼」，輸入原始註冊e-mail，系統將寄發通知信來重新設定密碼。";
        cloneContent.querySelector("#flush-headingSix").querySelector("button").innerHTML = "<li>如何修改會員資料？</li>";
        cloneContent.querySelector("#flush-collapseSix").querySelector("p").innerHTML = "登入平台後點選「會員資料管理」，選擇並完成想要修改的項目，填寫完後儲存即完成修改。";
        cloneContent.querySelector("#flush-headingSeven").querySelector("button").innerHTML = "<li>手機認證？</li>";
        cloneContent.querySelector("#flush-collapseSeven").querySelector("p").innerHTML = "<b>為什麼需要手機認證？</b><br>為了確保場地訂購機制不會被濫用，同時也保障雙方的權益，須留下手機號碼並進行驗證。<br><b>如何重新認證手機？</b><br>登入平台後點選「會員資料管理」，在「行動電話」欄位旁，點選「修改並重新驗證」，系統即會再次寄送驗證碼</p>";
        cloneContent.querySelector("#flush-headingEight").querySelector("button").innerHTML = "<li>沒收到新帳號認證信？</li>"
        cloneContent.querySelector("#flush-collapseEight").querySelector("p").innerHTML = "請先確認垃圾信件夾當中是否有會員認證信，因部份免費信箱系統會把平台通知信歸類到垃圾信件夾當中。請設定永遠對此一網域 (ZoneRadar.com)的接收許可。<br>如沒收到會員認證信，請至會員註冊頁面點選最下方「沒有收到註冊信？」，輸入註冊的E-mail，點選「發送驗證信」按鈕，系統將會再次發送會員認證信。";
        cloneContent.querySelector("#flush-headingNine").querySelector("button").innerHTML = "<li>如何註冊新帳號？</li>";
        cloneContent.querySelector("#flush-collapseNine").querySelector("p").innerHTML = "在首頁右上角點選「登入」圖示，可以直接使用Facebook、Gmail、LINE帳號登入，或點選「註冊新帳號」創建SpaceAdviosr會員帳號。";
        cloneContent.querySelector("#flush-headingTen").innerHTML = "";
        QALsit.append(cloneContent);
    }
    if (this.value == 3) {
        QALsit.innerHTML = "";
        let QA3 = $g('#QA-5');
        let cloneContent = QA3.content.cloneNode(true);
        cloneContent.querySelector("h1").innerHTML = "場地搜尋";
        cloneContent.querySelector("button").innerHTML = "<li>網站上的場地星級是依據什麼樣的標準制定？</li>";
        cloneContent.querySelector("p").innerHTML = "網頁上所標示的星級皆由該場地主根據其所在當地市場規範標準自行提供。ZoneRadar場地家不為任何住宿評定星級，且不承擔該住宿所自評的星級準確性的責任。";
        cloneContent.querySelector("#flush-headingTwo").querySelector("button").innerHTML = "<li>如何獲得停車場訊息？</li>";
        cloneContent.querySelector("#flush-collapseTwo").querySelector("p").innerHTML = "搜尋時，您可以在「設施」欄中選擇「停車」一項來輔助搜尋。場地主也會在主頁上「交通資訊」欄提供此類訊息。";

        cloneContent.querySelector("#flush-headingThree").querySelector("button").innerHTML = "<li>我如何得知場地主是否有某一設施或設備？</li>";
        cloneContent.querySelector("#flush-collapseThree").querySelector("p").innerHTML = "搜尋時，您可以在“設施”欄所列選項中選擇一項來輔助搜尋。場地主也會在主頁的“方案介紹”提供此類信息。<br>如沒有您想要的資訊，您可以與我們聯繫 <a href=#>service@ZoneRadar.com</a> 或 運用線上溝通系統直接發送訊息詢問場地主。</p>";


        cloneContent.querySelector("#flush-headingFour").querySelector("button").innerHTML = "<li>如何找到適合的場地？</li>";
        cloneContent.querySelector("#flush-collapseFour").querySelector("p").innerHTML = "直接在ZoneRadar首頁篩選所需要的場地條件：場地類別、活動性質、地區、人數、日期並輸入關鍵字，可進一步篩選出符合要求的場地。<br>或者也可以從首頁點選的主題專區，根據舉辦的活動類型找到最理想場地。";
        cloneContent.querySelector("#flush-collapseFive").innerHTML = "";
        QALsit.append(cloneContent);
    }
    if (this.value == 4) {
        QALsit.innerHTML = "";
        // containr.append().innerHTML="";
        let QA4 = $g('#QA-19');
        let cloneContent = QA4.content.cloneNode(true);
        cloneContent.querySelector("h1").innerHTML = "註冊/會員資料";
        cloneContent.querySelector("button").innerHTML = "<li>場地使用注意事項</li>";
        cloneContent.querySelector("p").innerHTML = "場地中的空間、設備設施等均屬於租用性質，敬請妥善使用、維護、保管。<br>如有任何物品損壞，請依照各場地主相關損壞賠償規定辦理。<br>各場地主的損害賠償規則不限於平台上公告之內容，如有疑義請逕洽各場地主諮詢。";
        cloneContent.querySelector("#flush-headingTwo").querySelector("button").innerHTML = "<li>什麼是「現場付款」？</li>";
        cloneContent.querySelector("#flush-collapseTwo").querySelector("p").innerHTML = "您訂購場地時，可以選擇場地使用當日再前往付款即可。亦將由場地主現場開立發票給您。<b>提醒您：請注意場地主現場可接受的收費方式。";
        cloneContent.querySelector("#flush-headingThree").querySelector("button").innerHTML = "<li>我要如何訂購多個場地？</li>";
        cloneContent.querySelector("#flush-collapseThree").innerHTML = "您可以將希望預定的場地加入購物車，從購物車中一起送出向場地主申請或是一起購買。";
        cloneContent.querySelector("#flush-headingFour").querySelector("button").innerHTML = "<li>我已在ZoneRadar場地家網站預訂場地，但是我發現了更便宜的價格</li>";
        cloneContent.querySelector("#flush-collapseFour").querySelector("p").innerHTML = "ZoneRadar場地家為您提供以下「最低價保證」。如果您已經透過ZoneRadar預訂場地，並在訂購後向我們證明您的確在其它網站上或是其他管道看到了相同日期、相同房型、相同條件下的更低預訂價格，在此情況下，我們將為您提供相同價格甚至更低價格。";
        cloneContent.querySelector("#flush-headingFive").querySelector("button").innerHTML = "<li>我是否可以向場地主直接進行預訂？</li>";
        cloneContent.querySelector("#flush-collapseFive").querySelector("p").innerHTML = "當然可以。但提醒您，ZoneRadar網站所提供的優惠價格，以及網站提供的積點服務只有透過我們預訂服務才有效。ZoneRadar積點將不限定場地，您所有的訂購都可以進行累積並享有回饋。<br>並且我們以第三方立場保障了您的訂購與使用權利，若有消費糾紛我們將協助您進行協商。";
        cloneContent.querySelector("#flush-headingSix").querySelector("button").innerHTML = "<li>訂購完成後可否變更訂單？</li>";
        cloneContent.querySelector("#flush-collapseSix").querySelector("p").innerHTML = "計畫還會變動？沒關係，您可以隨時變更訂單！您可隨時變更日期、時間、數量與場地。我們為您設計訂單隨時彈性調整的機制，讓您輕鬆訂購無負擔！";
        cloneContent.querySelector("#flush-headingSeven").querySelector("button").innerHTML = "<li>訂購完成後可否取消訂單？</li>";
        cloneContent.querySelector("#flush-collapseSeven").querySelector("p").innerHTML = "計畫還會變動？沒關係，您可以隨時取消訂單！付款後您享有的免費取消期限將依照場地主制定的期限。";
        cloneContent.querySelector("#flush-headingEight").querySelector("button").innerHTML = "<li>發出需求後如何更改需求？</li>";
        cloneContent.querySelector("#flush-collapseEight").innerHTML = "發出需求後若需更改，您可於訂單成立後進行變更，或是重新發送需求。";
        cloneContent.querySelector("#flush-headingNine").querySelector("button").innerHTML = "<li>發出需求後須等多久回覆？</li>";
        cloneContent.querySelector("#flush-collapseNine").querySelector("p").innerHTML = "如場地不可被線上即時訂購，則須發出需求等後場地主回覆確認訂單。<br>場地主通常於1-2個工作天內回覆您的訂單是否成立，如遇旺季或是訂單較多時，可能會延遲場地主回覆效率，還請您耐心等候！ 如您有較急的訂購需求或是已等候多時，歡迎您與場地家客服聯繫：<a href=mailto:service@ZoneRadarcom>service@ZoneRadar.com。</a>，我們將會盡快為您通知場地主。";
        cloneContent.querySelector("#flush-headingTen").querySelector("button").innerHTML = "<li>如何聯繫場地主？</li>";
        cloneContent.querySelector("#flush-collapseTen").querySelector("p").innerHTML = "訂單成立後，您可針對每一筆訂單分別與場地主聯繫訂單安排細節。相關的訊息紀錄會被記錄於系統中，保障您的權益。 登入會員後在訂單管理頁面找到指定訂單，點選「聯繫場地主」，即可與場地主聯繫。 訂單成立前無法提出詢問，如有任何問題歡迎與我們客服聯繫: <a href=mailto:service@ZoneRadar.com>service@ZoneRadar.com</a>";
        cloneContent.querySelector("#flush-headingEleven").querySelector("button").innerHTML = "<li>如何了解是否提供折扣優惠？</li>";
        cloneContent.querySelector("#flush-collapseEleven").querySelector("p").innerHTML = "所有的促銷價均為折後價格。如您有額外的折價券或折扣代碼，請於購買時輸入，金額將自動為您折抵。"
        cloneContent.querySelector("#flush-headingTwelve").querySelector("button").innerHTML = "<li>稅費和服務費金額是多少？</li>";
        cloneContent.querySelector("#flush-collapseTwelve").querySelector("p").innerHTML = "為方便您的交易，在您付費時，稅費和服務費會包括在內。各地的稅費由場地主自行定義，您可以在費用的後方看到稅費與服務費比例。";
        cloneContent.querySelector("#flush-headingThirteen").querySelector("button").innerHTML = "<li>價格是否包含稅費和服務費？</li>";
        cloneContent.querySelector("#flush-collapseThirteen").querySelector("p").innerHTML = "訂單明細的價格即為您所需支付的價格，價格已包含了所有額外費用。包含的額外費用將註明於價格後方，例如 NT$ Ten00 (含5%稅、Ten%服務費、清潔費...)";
        cloneContent.querySelector("#flush-headingFourteen").querySelector("button").innerHTML = "<li>我可否預訂未列出的價格方案？</li>";
        cloneContent.querySelector("#flush-collapseFourteen").querySelector("p").innerHTML = "若沒有找到您想要的價格方案 或 計費方式，可能是場地主尚未上架，您可以留下訊息，我們收到後將會為您確認並請場地主盡快上架，完成後將主動通知您前往訂購。";
        cloneContent.querySelector("#flush-headingFifteen").querySelector("button").innerHTML = "<li>為什麼同一場地有多種價格？</li>";
        cloneContent.querySelector("#flush-collapseFifteen").querySelector("p").innerHTML = "場地可以包裝許多套裝方案，每一個方案有不同的適用情境，也有不同的贈送內容。您可以選擇最優惠的方案訂購。提醒您：有部分的套裝有限定了使用身分，訂購前請注意方案說明，例如 謝師宴 (僅限學生身分訂購與使用)。";
        cloneContent.querySelector("#flush-headingSixteen").querySelector("button").innerHTML = "<li>價格是按人數計費、桌數計費還是按時間計？</li>";
        cloneContent.querySelector("#flush-collapseSixteen").querySelector("p").innerHTML = "請注意方案說明，有以人桌計費的方案 也有以時間計費的方案，您也可以篩選您想找的計費方式。";
        cloneContent.querySelector("#flush-headingSeventeen").querySelector("button").innerHTML = "<li>什麼是「即時預訂場地」？</li>";
        cloneContent.querySelector("#flush-collapseSeventeen").querySelector("p").innerHTML = "如果場地有標記閃電符號即為「即時預訂」，表示該場地不需要經過場地主核可，可供會員直接預訂，省略審核時間，讓預訂過程更省時。";
        cloneContent.querySelector("#flush-headingEighteen").querySelector("button").innerHTML = "<li>如何預訂場地？</li>";
        cloneContent.querySelector("#flush-collapseEighteen").querySelector("p").innerHTML = "<b>1. 提出申請</b><br>在場地頁面選擇訂購日期後，依照指示填寫時間及其他需求，點選「提出申請」按鈕，再輸入活動名稱及簡述，並輸入手機號碼，無誤後點選「發送驗證碼」，您的手機將會收到一組驗證碼，驗證碼輸入完成後，即完成「提出申請」的步驟，場地主通常在24小時內回覆是否可供租借。</p><p class=mb-0 text-secondary><b>2. 查看訂單</b><br>發出場地申請需求後，該筆訂單可在「我的訂單」頁面中查看，系統將會顯示審核中，不代表預訂成立，且暫時不收費。</p><p class=mb-0 text-secondary><b>3. 付款</b><br>如果場地主同意租借，系統將會再發訊息通知，可在「我的訂單」頁面中找到該筆訂單，在付款期限以前，點選「立即付款」後，按指示填寫資料，待付款成功後，才能完成場地的預訂。請注意，發出預訂請求不保證預訂成功，需待場地回覆確認，訂單才算成立。如訂單成立後，敬請於付款期限前完成付款，若逾期付款，系統將會自動取消訂單。";

        cloneContent.querySelector("#flush-headingNineteen").innerHTML = "";


        QALsit.append(cloneContent);
    }
    if (this.value == 5) {
        QALsit.innerHTML = "";
        let QA5 = $g("#QA-10");
        let cloneContent = QA5.content.cloneNode("true");
        cloneContent.querySelector("h1").innerHTML = "付款"
        cloneContent.querySelector("button").innerHTML = "<li>私下付款風險</li>";
        cloneContent.querySelector("p").innerHTML = "為保障您的交易安全，除了Space Advisor網站及應用程式外，請勿使用其他任何方式轉帳或與場地主連繫。<br>建議您全程使用ZoneRadar溝通。<br>若平台上的場地主要求您透過ZoneRadar網站及應用程式以外的方式付款（包含但不限於匯款單、銀行本票、速匯金、自由儲備、西聯匯款等），請向平台客服舉報，並且拒絕匯款，以免遭遇詐騙。";
        cloneContent.querySelector("#flush-headingTwo").querySelector("button").innerHTML = "<li>輸入信用卡的詳細訊息是否安全？</li>";
        cloneContent.querySelector("#flush-collapseTwo").querySelector("p").innerHTML = "ZoneRadar採用安全套接層 (SSL) 技術加密所有訊息，對您的資訊安全嚴密保護，請放心訂購。";
        cloneContent.querySelector("#flush-headingThree").querySelector("button").innerHTML = "<li>付款保證</li>";
        cloneContent.querySelector("#flush-collapseThree").querySelector("p").innerHTML = "訂購場地成功後，付款前都享有免費取消。<br>完成付款後則依據場地主的取消規則中的免費取消期限。";
        cloneContent.querySelector("#flush-headingFour").querySelector("button").innerHTML = "<li>超過期限未付款</li>";
        cloneContent.querySelector("#flush-collapseFour").querySelector("p").innerHTML = "超過付款期限仍未付款的訂單，系統將會自動取消訂單，請重新訂購。";
        cloneContent.querySelector("#flush-headingFive").querySelector("button").innerHTML = "<li>確認付款成功</li>";
        cloneContent.querySelector("#flush-collapseFive").querySelector("p").innerHTML = "付款成功之後，系統將會自動寄送信件到您在平台上登記的E-mail信箱，通知您已付款成功。同時也可在訂單管理系統點選「訂單資訊」查看付款狀態。";
        cloneContent.querySelector("#flush-headingSix").querySelector("button").innerHTML = "<li>付款期限？</li>";
        cloneContent.querySelector("#flush-collapseSix").querySelector("p").innerHTML = "場地家為確保消費者權益，平台保障付款期限的規則如下：<br>・如您的活動日為下訂後30天以上(含)，付款期限為訂單確認後，第7天內完成付款<br>・如您的活動日為下訂後29天內(含)，付款期限為訂單確認後，第3天內完成付款<br>・如您的活動日為下訂後2-3天內，付款期限為活動日前一天的17:00前，須完成付款<br>・如您的活動日為1天內，當天須完成付款<br<br>※每筆訂單付款期限，依據每個場地主所訂定的規則而有所不同，如果是需要等候場地主確認的訂單，您需等候場地主接受訂單後才可付款，每筆訂單成立後將有不同的付款日期，取決於場地主當日的訂購狀況。<br>如果是可立即確認的訂單，您則於訂購完成後就須完成付款以利場地保留，但您仍可於訂單成立後進行修改或取消。";
        cloneContent.querySelector("#flush-headingSeven").querySelector("button").innerHTML = "<li>稅費和服務費金額是多少？</li>";
        cloneContent.querySelector("#flush-collapseSeven").querySelector("p").innerHTML = "為方便您的交易，在您使用信用卡結算時，稅費和服務費會包括在內。稅費 與 服務費比例將依照各場地主自行設定的比例。";
        cloneContent.querySelector("#flush-headingEight").querySelector("button").innerHTML = "<li>如果我用來確定預訂的信用卡無效或被取消，我該怎麽辦？</li>";
        cloneContent.querySelector("#flush-collapseEight").querySelector("p").innerHTML = "如果您的信用卡無效或被取消，請聯絡我們以免您的訂單因扣款失敗而自動取消。";
        cloneContent.querySelector("#flush-headingNine").querySelector("button").innerHTML = "<li>有哪些付款方式？</li>";
        cloneContent.querySelector("#flush-collapseNine").querySelector("p").innerHTML = "<b>信用卡</b> 全額付清、信用卡分期、先付訂金</p><p class=mb-0 text-secondary><b>Visa金融卡(Debit卡)或銀行ATM轉帳</b> 消費前需先確認帳戶內有餘額才能使用，且金融卡無法使用銀行分期付款。如使用金融卡，請於扣款日前確保您的餘額充足，以防訂單因扣款失敗而被取消。</p><p class=mb-0 text-secondary><b>現場付款</b><br>若您選擇「現場付款」，需輸入您的卡號做為保證及存留用途，若當日未至現場完成付款，將從信用卡中扣款。 提醒您訂單總金額超過5萬元不提供現場付款的選項。";
        cloneContent.querySelector("#flush-headingTen").querySelector("button").innerHTML = "<li>預定場地需要支付哪些款項？</li>";
        cloneContent.querySelector("#flush-collapseTen").querySelector("p").innerHTML = "透過ZoneRadar搜尋場地、篩選場地、預訂場地，您不需要支付任何平台手續費，永久免手續費。訂單成立後才需支付您所訂購的場地相關費用。";
        QALsit.append(cloneContent);
    }
    if (this.value == 6) {
        QALsit.innerHTML = "";
        let QA6 = $g("#QA-19");
        let cloneContent = QA6.content.cloneNode(true);
        cloneContent.querySelector("h1").innerHTML = "訂單查詢/修改";
        cloneContent.querySelector("button").innerHTML = "<li>發票資訊錯誤</li>";
        cloneContent.querySelector("p").innerHTML = "如發生發票資訊錯誤的情況，請洽詢ZoneRadar客服 (service@ZoneRadar.com)，我們將有專人為您服務。";
        cloneContent.querySelector("#flush-headingTwo").querySelector("button").innerHTML = "<li>電子發票</li>";
        cloneContent.querySelector("#flush-collapseTwo").querySelector("p").innerHTML = "會員載具，若中獎將寄送至您所輸入的地址。";
        cloneContent.querySelector("#flush-headingThree").querySelector("button").innerHTML = "<li>三聯式發票</li>";
        cloneContent.querySelector("#flush-collapseThree").querySelector("p").innerHTML = "在訂購流程中填寫訂購資料時，可以直接選擇「三聯式發票」，再填寫發票抬頭與統一編號。平台依據財政部推行發票無紙化，採電子發票的形式進行發票作業，其效力與紙本發票相同，電子憑證也將寄送至您所登記的電子信箱。";
        cloneContent.querySelector("#flush-headingFour").querySelector("button").innerHTML = "<li>如何開立發票包含我公司名稱與統一編號？</li>";
        cloneContent.querySelector("#flush-collapseFour").querySelector("p").innerHTML = "請於付款時選擇三聯式發票，並填寫您的公司名稱與統一編號。";
        cloneContent.querySelector("#flush-headingFive").querySelector("button").innerHTML = "<li>如何查看發票？</li>";
        cloneContent.querySelector("#flush-collapseFive").querySelector("p").innerHTML = "您可至我的訂單中，查詢每筆訂單的發票。";
        cloneContent.querySelector("#flush-headingSix").querySelector("button").innerHTML = "<li>能否郵寄紙本收據？</li>";
        cloneContent.querySelector("#flush-collapseSix").querySelector("p").innerHTML = "ZoneRadar全面使用電子發票，無實體發票。我們會主動協助兌獎，並於中獎時通知兌獎事宜。如您選擇開立三聯式發票，我們將mail憑證給您，請注意您的電子郵件信箱。";
        cloneContent.querySelector("#flush-headingSeven").querySelector("button").innerHTML = "<li>私訊功能</li>";
        cloneContent.querySelector("#flush-collapseSeven").querySelector("p").innerHTML = "▲ 查看/回覆訊息<br>登入會員後，在首頁右上角點選信件夾圖示，即可進入收件夾查看與回覆系統通知和站內訊息。<br>請注意，訂單成立後才可進行安排內容的詳細溝通";
        cloneContent.querySelector("#flush-headingEight").querySelector("button").innerHTML = "<li>當日使用場地時要向場地主出示什麽？</li>";
        cloneContent.querySelector("#flush-collapseEight").querySelector("p").innerHTML = "依據每個場地主的規則不同，一般而言您需要出示：<br>- ZoneRadar場地家訂購憑證或訂單編號<br>- 預訂時所用的信用卡<br>- 有效身份證件";
        cloneContent.querySelector("#flush-headingNine").querySelector("button").innerHTML = "<li>修改訂單</li>";
        cloneContent.querySelector("#flush-collapseNine").querySelector("p").innerHTML = "成功預訂場地後，可以再進行人數/桌數、時間等安排的修改。<br>您可至「我的訂單」找到訂單，並進行需求變更。<br>提醒您，減少的部份視同取消，取消金比照取消條款辦理。";
        cloneContent.querySelector("#flush-headingTen").querySelector("button").innerHTML = "<li>如何得知我的預訂已被確認？</li>";
        cloneContent.querySelector("#flush-collapseTen").querySelector("p").innerHTML = "您會在預訂成功後 3 分鐘內收到該電子郵件。如果您未收到訂購確認通知，請檢查您的垃圾郵件和/或垃圾郵件過濾器。您也可以透過網站登入「我的預訂」頁面來檢查您的預訂狀態。";
        cloneContent.querySelector("#flush-headingEleven").querySelector("button").innerHTML = "<li>為什麽場地主無預訂記錄？</li>";
        cloneContent.querySelector("#flush-collapseEleven").querySelector("p").innerHTML = "所有預訂都會立即確認。如果場地主沒有您的預訂記錄，請立即聯絡ZoneRadar。";
        cloneContent.querySelector("#flush-headingTwelve").querySelector("button").innerHTML = "<li>如何更換場地主或方案？</li>";
        cloneContent.querySelector("#flush-collapseTwelve").querySelector("p").innerHTML = "更換場地主/方案需要取消您的原始預訂並重新預訂。請注意，取消任何預訂均須依據場地主的取消預訂政策進行。";
        cloneContent.querySelector("#flush-headingThirteen").querySelector("button").innerHTML = "<li>可否加購餐飲、住宿或設備？例如投影機或麥克風？</li>";
        cloneContent.querySelector("#flush-collapseThirteen").querySelector("p").innerHTML = "在場地主有販售的情況下，您可以於訂購當下 或是訂購完成後，選擇加購餐飲、住宿或設備。";
        cloneContent.querySelector("#flush-headingFourteen").querySelector("button").innerHTML = "<li>查詢訂單狀態</li>";
        cloneContent.querySelector("#flush-collapseFourteen").querySelector("p").innerHTML = "進入<b>「我的訂單」</b>頁面，會顯示五種不同的訂單狀態：</p><p class=mb-0 text-secondary><br>1.審核中 若訂單尚未經過場地主審核，訂單將出現在「審核中」分頁。<p class=mb-0 text-secondary><br>2.待付款 若訂單已經過場地主審核，但尚未完成付款，訂單將出現在「待付款」分頁</p><p class=mb-0 text-secondary><br>3.已付款 若訂單已完成付款，訂單將出現在「已付款」分頁</p><p class=mb-0 text-secondary><br>4.已結束 若訂單的活動日期已結束，訂單將出現在「已結束」分頁</p><p class=mb-0 text-secondary><br>5.已取消 若訂單已取消，訂單將出現在「已取消」的分頁。</p><p class=mb-0 text-secondary><br>▲ 若想查看每筆訂單的詳情，點選「明細管理」，則可看到訂單的詳細資訊。";
        cloneContent.querySelector("#flush-headingFifteen").querySelector("button").innerHTML = "<li>發票何時會開立？</li>";
        cloneContent.querySelector("#flush-collapseFifteen").querySelector("p").innerHTML = "發票將統一於活動結束後一日開立。";
        cloneContent.querySelector("#flush-headingSixteen").querySelector("button").innerHTML = "";
        cloneContent.querySelector("#flush-headingSeventeen").querySelector("button").innerHTML = "";
        cloneContent.querySelector("#flush-headingEighteen").querySelector("button").innerHTML = "";
        cloneContent.querySelector("#flush-headingNineteen").querySelector("button").innerHTML = "";
        QALsit.append(cloneContent);
    }
    if (this.value == 7) {
        QALsit.innerHTML = "";
        // containr.append().innerHTML="";
        let QA7 = $g('#QA-2');
        let cloneContent = QA7.content.cloneNode(true);
        // QA titel
        cloneContent.querySelector("h1").innerText = "取消預定";
        // QA
        cloneContent.querySelector("button").innerHTML = "<li>取消訂單</li>";
        cloneContent.querySelector("p").innerHTML = "ZoneRadar為您提供自助服務選項。您可以透過網站中的「我的預訂」頁面進行取消預訂。系統將為您試算應退款或產生的費用。</p><p><br><b>怎樣正確計算取消預訂日期？</b><br>取消預訂政策中所表示的取消天數規範須以您預訂場地的當地日曆天為計算標準，即表示如果過了當地時間午夜12點則須以隔天計算。</p><p><br><b>取消訂單後，折扣代碼仍可使用嗎？</b><br>取消預訂時，當筆訂單將不可再使用折扣代碼，亦不再享有折扣的優惠金額。 若您取消訂單，原折扣代碼將退還至您的帳戶中，您可於折扣代碼使用期限內使用至其他訂單中。</p><p><br><b>取消費用計算</b><br>多數訂單都包含免費取消的期限。根據場地使用時段、場地類型、促銷活動等等，每次預訂的取消政策的細節和條款均有所不同。您可以於訂單明細中查看您可免費取消的期限，如已過免費取消期限則需依據場地主的取消政策支付取消費用。</p><p><br><b>取消需要再付款的情況</b><br>若您訂單預訂時有使用折扣代碼，如欲取消將先扣除折扣代碼的優惠金額，再進行取消金額計算，若可退款的金額小於折扣代碼金額，就需要再另行支付差額。</p><p><br><b>取消成功後的退款方式</b><br>如您的訂單是使用信用卡付款，當您的訂單成功取消後，應退款項將於次一個結帳日退回至您的信用卡帳戶中。<br>如您是使用ATM付款，當您的訂單成功取消後，請與客服聯繫並告知您的退款帳號，請注意，系統僅接受與訂購者本人相同名稱之銀行帳戶。<br>如付款日與取消日間隔一年以上的訂單，由於金流作業的限制，如欲取消，請與客服聯繫。<br>若有其他問題請洽ZoneRadar場地家客服人員。</p>";
        cloneContent.querySelector("#flush-headingTwo").querySelector("button").innerHTML = "<li>免費取消期限說明</li>";
        cloneContent.querySelector("#flush-collapseTwo").querySelector("p").innerText = "如您訂購一個月以後的場地，則可享有平台保障7天內免費取消的權益，若超過7天以上欲取消場地，則依照場地主訂定的取消金比例計算。<br>注意：取消金額與取消日相關，如欲取消建議您盡早取消以降低取消金。<br>如您訂購一個月以內的場地，如付款後欲取消，則依場地主訂定的取消比例計算之。<br>場地主的取消比例皆按照距離活動日的天數收取不同比例的手續費，而每間場地的退款規定皆不相同，(詳細內容請參考場地頁中的取消條款說明)";
        QALsit.append(cloneContent);
    }
    if (this.value == 8) {
        QALsit.innerHTML = "";
        let QA8 = $g("#QA-5");
        let cloneContent = QA8.content.cloneNode(true);
        cloneContent.querySelector("h1").innerHTML = "場地評論"
        cloneContent.querySelector("button").innerHTML = "<li>如何分享場地評論？</li>";
        cloneContent.querySelector("p").innerHTML = "您可以在場地頁面下方點選「評價」撰寫您對場地的心得，或者在訂單頁面點選「前往評價」，會員每對一個場地進行評論，即可獲得1點評論點數。<br>會員帳號每日最多只能評論3個場地，且每日可獲得的評論點數上限為3點。<br>詳細說明請至活動說明頁面查詢。 <a href=https://www.ZonRadar.com/>https://www.ZonRadar.com/</a> ";
        cloneContent.querySelector("#flush-headingTwo").querySelector("button").innerHTML = "<li>如何檢舉評論？</li>";
        cloneContent.querySelector("#flush-collapseTwo").querySelector("p").innerHTML = "您可以在場地頁面下方點選「評價」撰寫您對場地的心得，或者在訂單頁面點選「前往評價」，會員每對一個場地進行評論，即可獲得1點評論點數。<br>會員帳號每日最多只能評論3個場地，且每日可獲得的評論點數上限為3點。<br>詳細說明請至活動說明頁面查詢。 <a href=https://www.ZonRadar.com/ >https://www.ZonRadar.com/ </a>";
        cloneContent.querySelector("#flush-headingThree").querySelector("button").innerHTML = "<li>評論注意事項</li>";
        cloneContent.querySelector("#flush-collapseThree").querySelector("p").innerHTML = "請填寫符合您真實經歷的場地評論，並評價星等，評論字數至少需50字以上，提醒您評論內容不得涉及不雅用語、人身攻擊、敏感題材或其他不適當內容。<br>請不要包含地址和電話號碼等個人信息。<br>違反審查規範或我們網站的服務條款以及其他政策的評論可能會被拒絕發布或刪除。評論前請查閱評論聲明注意事項 <a href=https://www.ZonRadar.com/reward>https://www.ZonRadar.com/reward</a><br><br>您的評論發布後，將經過3-5個工作天審查，一經審查通過將自動公開發佈。評論一經發布將不可刪除。";
        cloneContent.querySelector("#flush-headingFour").querySelector("button").innerHTML = "<li>場地評論的好處</li>";
        cloneContent.querySelector("#flush-collapseFour").querySelector("p").innerHTML = "透過您分享的評論，可以讓更多場地租用者事先了解場地狀況。同樣您也可以參考其他網友在場地留下的評論，找到理想中的場地喔！";
        cloneContent.querySelector("#flush-headingFive").innerHTML = "";

        QALsit.append(cloneContent);
    }
    if (this.value == 9) {
        QALsit.innerHTML = "";
        let QA9 = $g("#QA-5");
        let cloneContent = QA9.content.cloneNode(true);
        cloneContent.querySelector("h1").innerHTML = "取消預定";
        cloneContent.querySelector("button").innerHTML = "<li>取消訂單</li>";
        cloneContent.querySelector("p").innerHTML = "SpaceAdvisor為您提供自助服務選項。您可以透過網站中的「我的預訂」頁面進行取消預訂。系統將為您試算應退款或產生的費用。</p><p class=mb-0 text-secondary><br><b>怎樣正確計算取消預訂日期？</b><br>取消預訂政策中所表示的取消天數規範須以您預訂場地的當地日曆天為計算標準，即表示如果過了當地時間午夜12點則須以隔天計算。</p><p class=mb-0 text-secondary><br><b>取消訂單後，折扣代碼仍可使用嗎？</b><br>取消預訂時，當筆訂單將不可再使用折扣代碼，亦不再享有折扣的優惠金額。 若您取消訂單，原折扣代碼將退還至您的帳戶中，您可於折扣代碼使用期限內使用至其他訂單中。</p><p class=mb-0 text-secondary><br><b>取消費用計算</b><br>多數訂單都包含免費取消的期限。根據場地使用時段、場地類型、促銷活動等等，每次預訂的取消政策的細節和條款均有所不同。您可以於訂單明細中查看您可免費取消的期限，如已過免費取消期限則需依據場地主的取消政策支付取消費用。</p><p class=mb-0 text-secondary><br><b>取消需要再付款的情況</b><br>若您訂單預訂時有使用折扣代碼，如欲取消將先扣除折扣代碼的優惠金額，再進行取消金額計算，若可退款的金額小於折扣代碼金額，就需要再另行支付差額。";
        cloneContent.querySelector("#flush-headingTwo").querySelector("button").innerHTML = "<li>異業合作聯絡管道</li>";
        cloneContent.querySelector("#flush-collapseTwo").querySelector("p").innerHTML = "請洽客服中心：<a href=mailto:service@ZonRadar.com?subject=【異業合作聯絡】>service@ZonRadar.com</a>";
        cloneContent.querySelector("#flush-headingThree").querySelector("button").innerHTML = "<li>場地業者聯絡管道</li>";
        cloneContent.querySelector("#flush-collapseThree").querySelector("p").innerHTML = "如欲申請成為場地主，請先填寫場地主申請資料：<a href=http://support@ZonRadar.com>https://www.ZonRadar.com/space/readme</a></p><p class=mb-0 text-secondary>如欲詢問其他事宜， 請洽場地主客戶服務中心：<a href=mailto:support@ZonRadar.com?subject=【場地業者聯絡管道】>support@ZonRadar.com</a>";
        cloneContent.querySelector("#flush-headingFour").querySelector("button").innerHTML = "<li>一般會員聯絡管道</li>";
        cloneContent.querySelector("#flush-collapseFour").querySelector("p").innerHTML = ">請洽客服中心：<a href=mailto:service@ZonRadar.com>service@ZonRadar.com</a>";

        cloneContent.querySelector("#flush-headingFive").innerHTML = "";
        QALsit.append(cloneContent);
    }
    if (this.value == 10) {
        QALsit.innerHTML = "";
        let QA10 = $g('#QA-2')
        let cloneContent = QA10.content.cloneNode(true);
        cloneContent.querySelector("h1").innerText = "場地資訊錯誤回報";
        cloneContent.querySelector("button").innerHTML = "<li>場地資訊錯誤回報</li>";
        cloneContent.querySelector("p").innerHTML = "請洽客服中心：<a href=mailto:service@ZoneRadar.com?subject=【場地資訊錯誤回報】>service@ZoneRadar.com</a>";
        cloneContent.querySelector("#flush-headingTwo").innerHTML = "";
        QALsit.append(cloneContent);
    }


})


