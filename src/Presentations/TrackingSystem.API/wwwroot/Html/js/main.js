//qrcode.clear(); // clear the code.
//qrcode.makeCode("http://naver.com");

Vue.prototype.$bus = new Vue();

var statusInstance = new Vue({
  el: "#info",
  template: "<div id=\"info\"> <p>LICENSE: <span :class=\"{'green': licenseStatus, 'red': !licenseStatus}\">{{licenseStatus ? 'ACTIVE' : 'INACTIVE'}}</span></p>" +
            "<p>SERVER INSTANCE NAME: <span v-if=\"machineName && machineName.length\">{{ machineName }}</span></p>" +
            "<p>API SERVER: <span :class=\"{'green': api, 'red': !api}\">{{api ? 'OK' : 'NOK'}}</span></p>" +
            "<p>CONNECTION: <span :class=\"{'green': mainConn, 'red': !mainConn}\">{{mainConn ? 'OK' : 'NOT OK'}}</span></p>" +
            "<p>TASK MANAGER CONNECTION: <span :class=\"{'green': tmConn, 'red': !tmConn}\">{{tmConn ? 'OK' : 'NOT OK'}}</span></p>" +
            "<p>DATABASE MIGRATIONS: <span v-if=\"pendingMigrations === 0\" class=\"green\">OK<span v-if=\"lastExecutedMigration\"> ({{ lastExecutedMigration }})</span></span><span v-if=\"pendingMigrations > 0\" class=\"red\">ERROR (OSTATNIA WDROŻONA: {{ lastExecutedMigration }}, POZOSTAŁO: {{ pendingMigrations }})</span> </span></p>" +
            "<p>APPLICATION VERSION: {{ appVersion }}</span></p>" +
            "<p v-if=\"!licenseStatus || activateNew\" >HARDWARE ID: {{ hardwareId }}</p>" +
            "<button v-if=\"!licenseStatus || activateNew\" class=\"btn\" @click=\"copyToClipboard(hardwareId)\" style=\"margin-bottom: 25px; padding: 0 15px;\">Copy Hardware ID</button>" +
            "<br>" +
            "<div id=\"qrcode\" v-if=\"applicationUrl.length\"></div>" +
            "<br>" +
            "</div>",
  data: {
    licenseStatus: STATUS.license,
    api: STATUS.api,
    mainConn: STATUS.mainConn,
    tmConn: STATUS.tmConn,
    appVersion: STATUS.appVersion,
    applicationUrl: STATUS.applicationUrl,
    machineName: STATUS.machineName,
    status: STATUS,
    pendingMigrations: STATUS.pendingMigrations,
    lastExecutedMigration: STATUS.lastExecutedMigration,
    hardwareId: STATUS.hardwareId,
    activateNew: false
  },
    methods: {
        copyToClipboard(text) {
            navigator.clipboard.writeText(text);
        }
    },
  mounted: function() {
    var self = this;
      document.title = "Warehouse API";
      console.log(STATUS);
      var qrcode = new QRCode("qrcode", {
      text: this.applicationUrl,
      width: 128,
      height: 128,
      colorDark: "#000000",
      colorLight: "#ffffff",
      correctLevel: QRCode.CorrectLevel.H
    });

    this.$bus.$on("activateNew", (value) => {
        this.activateNew = value
    })
  }
});

activationInstance = new Vue({
  el: "#activate",
  template: "<footer v-if=\"licenseStatus === false || activateNew === true\" id=\"activate\">" +
                "<div v-if=\"loader === true\" >" +
                    "<div class=\"lds-facebook\"><div></div><div></div><div></div></div>" +
                "</div>" +
                "<div v-if=\"loader === false\">" +
                    "<div class=\"activation-type\" style=\"\">" +
                        "<div><input type=\"radio\" id=\"radioOnline\" name=\"activationType\" value=\"online\" v-model=\"activationType\"><label for=\"radioOnline\">Online Activation</label></div>"+
                        "<div><input type=\"radio\" id=\"radioOffline\" name=\"activationType\" value=\"offline\" v-model=\"activationType\"><label for=\"radioOffline\">Offline Activation</label></div>"+
                    "</div>" +
                    "<hr>" +
                    "<div v-if=\"activationType == 'online'\">" +
                        "<h2>Enter online license key</h2>" +
                        "<div class=\"form\">" +
                            "<input type=\"text\" v-model=\"licenseNumber\" v-on:keyup=\"check\" style=\"border-width: 1.5px\" :style=\"{borderColor: borderColor}\">" +
                            "<input type=\"submit\" value=\"Activate\" @click=\"activate\">" +
                        "</div>" +
                    "</div>" +
                    "<div v-if=\"activationType == 'offline'\">" +
                        "<h2>Enter offline license key</h2>" +
                        "<div class=\"form\" style=\"display: flex; flex-direction: column;\">" +
                            "<textarea style=\"border-width: 1.5px\" :style=\"{borderColor: borderColor}\" v-model=\"offlineLicenseString\" rows=\"8\"></textarea><br>" +
                            "<input type=\"submit\" value=\"Activate\" @click=\"activate\">" +
                        "</div>" +
                    "</div>" +
                "</div>" +
      "</footer>" +
      "<footer v-else id=\"activate\">" +
        "<button  class=\"btn\" @click=\"$bus.$emit('activateNew', true)\" style=\"padding: 0 15px;\">Activate New License</button>" +
      "</footer>",

    data: {
        status: STATUS,
        licenseStatus: STATUS.license,
        borderColor: '',
        licenseNumber: '',
        offlineLicenseString: '',
        activationType: 'online',
        loader: false,
        activateNew: false
    },
    watch: {
        'activationType'() {
            this.licenseNumber = null;
            this.offlineLicenseString = null;
        }
    },
    methods: {
        activate: function () {
            const self = this;
            this.loader = true;
            console.log("click" + window.location.href);
            var url = window.location.href + "api/license-activation";
            var licenseData = { licenseNumber: this.licenseNumber, offlineLicenseString: this.offlineLicenseString };
            var activatePromise = jQuery.ajax({
                type: "POST",
                url: url,
                contentType: "application/json",
                data: JSON.stringify(licenseData)
            });

            activatePromise.done(function (data) {
                let activationResponse = data;
                if (activationResponse.isError) {
                    alert(activationResponse.errorMsg.Message_en_EN);
                } else {
                    activationInstance.licenseStatus = true;
                    statusInstance.licenseStatus = true;
                    self.$bus.$emit('activateNew', false);
                }

                activationInstance.loader = false;
            });

            activatePromise.fail(function (data) {
                activationInstance.loader = false;
                console.log("fail");
                alert("License activation was not successful.");
            });
        },
        check: function () {
            if (this.licenseNumber.length == 24) {
                this.borderColor = "green";
            } else {
                this.borderColor = "red";
            }
        },
    },
    mounted: function () {
        this.$bus.$on("activateNew", (value) => {
            this.activateNew = value
        })
    }
});
