
(function () {
  // DOM元素
  const imageInput = document.querySelector('#image-input');
  const fileSelect = document.querySelector('#file-select');
  const previewZone = document.querySelector('#preview-zone');
  const imgSubmitBtn = document.querySelector('.img-upload-btn');

  let name, preset;
  let originImgs = [];
  // spaceID需先存在razor page裡
    //let spaceID = 168;
    //let  spaceID = parseInt(document.querySelector('#mySpaceId').innerHTML);
  //  let spaceID =4;
  // function定義
  // 從後端獲得上傳所需資訊
  //function getPrams() {
  //    axios.get(`/webapi/spaces/GetUploadPrams?id=${spaceID}`).then(res => {
  //    name = res.data.Response.Name;
  //    preset = res.data.Response.Preset;
  //    originImgs = res.data.Response.PhotoUrlList;
  //    console.log(res.data.Response);
  //    if (originImgs.length) {
  //      originImgs.forEach(url => {
  //        showThumbnail(url)
  //      })
  //    }
  //  }).catch(err => console.log(err))
  //}

  // 設定可拖曳的區域
  function setSortable() {
    let sortable = new Sortable(previewZone, {
      animation: 150,
      ghostClass: 'ghost',
      onChange: function () {
        imgSubmitBtn.removeAttribute('disabled');
      },
    })
  }

  // 使用按鈕上傳檔案
  function setUploadBtn() {
    fileSelect.addEventListener('click', function (e) {
      if (imageInput) {
          imageInput.click();
      }
      e.preventDefault();
    }, false);
  }

  // 抓取準備要上傳的物件
  function queueAndProcess() {
    if (!this.files.length) return;

    imgSubmitBtn.removeAttribute('disabled');
    for (let i = 0; i < this.files.length; i++) {
      upload(this.files[i]);
    }
  }

  // 上傳圖片至cloudinary
  function upload(file) {
    const url = `https://api.cloudinary.com/v1_1/${name}/upload`;
    let formData = new FormData();
    formData.append('file', file);
    formData.append('upload_preset', preset);

    axios({
      url: url,
      method: 'POST',
      headers: {
        'Content-Type': 'application/x-www-form-urlencoded',
      },
      data: formData,
    }).then(res => {
      let imgUrl = res.data.secure_url;
      showThumbnail(imgUrl);
    });
  }

  // 產生縮圖
  function showThumbnail(imgUrl) {
    const imgTemplate = document.querySelector('#placeholder-template').content.cloneNode(true);
    const thumbnail = imgTemplate.querySelector('.thumbnail-holder');
    const closeBtn = imgTemplate.querySelector('.thumbnail-holder .fa-times');

    thumbnail.style.backgroundImage = `url("${imgUrl}")`;
    
    thumbnail.dataset.url = imgUrl;
    imgTemplate.classList = 'wrap p-0 mb-4 mx-3 border border-3 rounded';
    thumbnail.classList = 'thumbnail-holder';
    closeBtn.classList = 'fas fa-times';

    previewZone.appendChild(imgTemplate);
    closeBtn.addEventListener('click', removeThumbnail);
  }

  // 隱藏取消的縮圖
  function removeThumbnail() {
    this.parentNode.parentNode.classList.add('d-none');
    this.removeEventListener('click', removeThumbnail);
  }

  // 取得並存入所有剩下的圖片
  function transferData() {
    let imgGroup = previewZone.querySelectorAll('div:not(.d-none) > .thumbnail-holder');

    let SaveSpacePhotosVM = {
        SpaceID: spaceID,
      PhotoUrlList: [],
    }
    imgGroup.forEach(node => {
      SaveSpacePhotosVM.PhotoUrlList.push(node.dataset.url);
    })

    axios.post('/webapi/spaces/SavePhotos', SaveSpacePhotosVM).then(res => {
      console.log(res);
      Swal.fire(
        '上傳成功',
        '請繼續填寫您的場地資訊!!!',
        'success'
      )
    }).catch(err => console.log(err));
  }

  // 執行區
  //getPrams();
  setSortable();
  // previewZone.addEventListener('click', checkUpdate, false);
  setUploadBtn();
  imageInput.addEventListener('change', queueAndProcess, false);
  imgSubmitBtn.addEventListener('click', transferData, false);

})();