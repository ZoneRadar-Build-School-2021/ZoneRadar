    let app=new Vue({
        el:'#app',
    data:{
    isVerify:true,
        inputData: {
            //1.
            address: "",
            //2.
            SpaceName:"",
            MeasureOfArea:"",
            Capacity: "",
            //3.
            SpacePrice:"",
            minSpacePrice: "",
            Discount:"",
                    },
    inputDataCheck:{
            addressError:false,
            addressErrormsg:'',
            SpaceNameError:false,
            SpaceNameErrormsg:"",
            MeasureOfAreaError:false,
            MeasureOfAreaErrormsg: "",
            CapacityError: false,
            CapacityErrormsg: "",
            SpacePriceError: false,
            SpacePriceErrormsg: "",
            minSpacePriceError: false,
            minSpacePriceErrormsg: "",
            DiscountError: false,
            DiscountErrormsg: "",
                },
            },
    watch:{

    // 'inputData.Account':function(){
        'inputData.Account':{
        immediate:true,
            handler() {
                let addressRegxp = /路/
        //不為空 
        if (this.inputData.address=='') {
            this.inputDataCheck.addressError = true;
            this.inputDataCheck.addressErrormsg="不能為空"
        } else if (this.inputData.address.length<3) {
            this.inputDataCheck.addressError = true;

            this.inputDataCheck.addressErrormsg="地址至少提供至路名"
        }
        else {
            this.inputDataCheck.addressError = false;
            this.inputDataCheck.addressErrormsg="";
                        }
            this.checkverify();
                    }
                },
    'inputData.Password':{
        immediate:true,
    handler(){
        let passwordRegxp=/^[0-9]{1}[0-9]*$/
    if (!passwordRegxp.test(this.inputData.Password))
    {
        this.inputDataCheck.PasswordError = true;
    this.inputDataCheck.PasswordErrormsg="數字"
                        }else
    {
        this.inputDataCheck.PasswordError = false;
    this.inputDataCheck.PasswordErrormsg=""
                        }
                    } 
                }
            },
    computed:{

    },
    methods:{
        checkverify(){
                    for (let prop in this.inputDataCheck) {
                        if (this.inputDataCheck[prop]==true) {
        this.isVerify = false;
    return;
                        }
    this.isVerify=true;
                    }
                }
            }
        })
