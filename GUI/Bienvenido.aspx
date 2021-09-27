<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Bienvenido.aspx.cs" Inherits="GUI.Bienvenido" MasterPageFile="~/Site.Master" %>

<%@ Register Src="~/User_Controls/UC_MensajeModal.ascx" TagPrefix="uc1" TagName="UC_MensajeModal" %>


<asp:Content ID="MainContent" runat="server" ContentPlaceHolderID="MainContent">
    <br />
    <!-------Banner Script/Style---------->
    <script src="Scripts/jssor.slider-23.1.5.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        jssor_1_slider_init = function () {

            var jssor_1_SlideoTransitions = [
                [{ b: 900, d: 2000, x: -379, e: { x: 7 } }],
                [{ b: 900, d: 2000, x: -379, e: { x: 7 } }],
                [{ b: -1, d: 1, o: -1, sX: 2, sY: 2 }, { b: 0, d: 900, x: -171, y: -341, o: 1, sX: -2, sY: -2, e: { x: 3, y: 3, sX: 3, sY: 3 } }, { b: 900, d: 1600, x: -283, o: -1, e: { x: 16 } }]
            ];

            var jssor_1_options = {
                $AutoPlay: 1,
                $SlideDuration: 800,
                $SlideEasing: $Jease$.$OutQuint,
                $CaptionSliderOptions: {
                    $Class: $JssorCaptionSlideo$,
                    $Transitions: jssor_1_SlideoTransitions
                },
                $ArrowNavigatorOptions: {
                    $Class: $JssorArrowNavigator$
                },
                $BulletNavigatorOptions: {
                    $Class: $JssorBulletNavigator$
                }
            };

            var jssor_1_slider = new $JssorSlider$("jssor_1", jssor_1_options);

            /*responsive code begin*/
            /*remove responsive code if you don't want the slider scales while window resizing*/
            function ScaleSlider() {
                var refSize = jssor_1_slider.$Elmt.parentNode.clientWidth;
                if (refSize) {
                    refSize = Math.min(refSize, 1920);
                    jssor_1_slider.$ScaleWidth(refSize);
                }
                else {
                    window.setTimeout(ScaleSlider, 30);
                }
            }
            ScaleSlider();
            $Jssor$.$AddEvent(window, "load", ScaleSlider);
            $Jssor$.$AddEvent(window, "resize", ScaleSlider);
            $Jssor$.$AddEvent(window, "orientationchange", ScaleSlider);
            /*responsive code end*/
        };
    </script>
    <style>
        /* jssor slider bullet navigator skin 05 css */
        /*
        .jssorb05 div           (normal)
        .jssorb05 div:hover     (normal mouseover)
        .jssorb05 .av           (active)
        .jssorb05 .av:hover     (active mouseover)
        .jssorb05 .dn           (mousedown)
        */
        .jssorb05 {
            position: absolute;
        }

            .jssorb05 div, .jssorb05 div:hover, .jssorb05 .av {
                position: absolute;
                /* size of bullet elment */
                width: 16px;
                height: 16px;
                background: url('Imagenes/buttons_banner.png') no-repeat;
                overflow: hidden;
                cursor: pointer;
            }

            .jssorb05 div {
                background-position: -7px -7px;
            }

                .jssorb05 div:hover, .jssorb05 .av:hover {
                    background-position: -37px -7px;
                }

            .jssorb05 .av {
                background-position: -67px -7px;
            }

            .jssorb05 .dn, .jssorb05 .dn:hover {
                background-position: -97px -7px;
            }

        /* jssor slider arrow navigator skin 22 css */
        /*
        .jssora22l                  (normal)
        .jssora22r                  (normal)
        .jssora22l:hover            (normal mouseover)
        .jssora22r:hover            (normal mouseover)
        .jssora22l.jssora22ldn      (mousedown)
        .jssora22r.jssora22rdn      (mousedown)
        .jssora22l.jssora22lds      (disabled)
        .jssora22r.jssora22rds      (disabled)
        */
        .jssora22l, .jssora22r {
            display: block;
            position: absolute;
            /* size of arrow element */
            width: 40px;
            height: 58px;
            cursor: pointer;
            background: url('Imagenes/arrows_banner.png') center center no-repeat;
            overflow: hidden;
        }

        .jssora22l {
            background-position: -10px -31px;
        }

        .jssora22r {
            background-position: -70px -31px;
        }

        .jssora22l:hover {
            background-position: -130px -31px;
        }

        .jssora22r:hover {
            background-position: -190px -31px;
        }

        .jssora22l.jssora22ldn {
            background-position: -250px -31px;
        }

        .jssora22r.jssora22rdn {
            background-position: -310px -31px;
        }

        .jssora22l.jssora22lds {
            background-position: -10px -31px;
            opacity: .3;
            pointer-events: none;
        }

        .jssora22r.jssora22rds {
            background-position: -70px -31px;
            opacity: .3;
            pointer-events: none;
        }
    </style>
    <!-------Banner Script/Style---------->

    <div id="jssor_1" style="position: relative; margin: 0 auto; top: 0px; left: 0px; width: 800px; height: 200px; overflow: hidden; visibility: hidden;">
        <!-- Loading Screen -->
        <div data-u="loading" style="position: absolute; top: 0px; left: 0px; background-color: rgba(0,0,0,0.7);">
            <div style="filter: alpha(opacity=70); opacity: 0.7; position: absolute; display: block; top: 0px; left: 0px; width: 100%; height: 100%;"></div>
            <div style="position: absolute; display: block; background: url('ing/loading.gif') no-repeat center center; top: 0px; left: 0px; width: 100%; height: 100%;"></div>
        </div>
        <div data-u="slides" style="cursor: default; position: relative; top: 0px; left: 0px; width: 800px; height: 200px; overflow: hidden;">
            <div>
                <img data-u="image" src="Imagenes/banner1.jpg" />
            </div>
            <div>
                <img data-u="image" src="Imagenes/banner2.jpg" />
            </div>
            <div>
                <img data-u="image" src="Imagenes/banner3.jpg" />
            </div>
            <div>
                <img data-u="image" src="Imagenes/banner4.jpg" />
            </div>
            <div>
                <img data-u="image" src="Imagenes/banner5.jpg" />
            </div>
        </div>
        <!-- Bullet Navigator -->
        <div data-u="navigator" class="jssorb05" style="bottom: 16px; right: 16px;" data-autocenter="1">
            <!-- bullet navigator item prototype -->
            <div data-u="prototype" style="width: 16px; height: 16px;"></div>
        </div>

        <!-- Arrow Navigator -->
        <span data-u="arrowleft" class="jssora22l" style="top: 0px; left: 8px; width: 40px; height: 58px;" data-autocenter="2"></span>
        <span data-u="arrowright" class="jssora22r" style="top: 0px; right: 8px; width: 40px; height: 58px;" data-autocenter="2"></span>
    </div>

    <div class="modal fade" id="MensajeModal" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <uc1:UC_MensajeModal runat="server" ID="UC_MensajeModal" />
        </div>
    </div>

    <script type="text/javascript">
        function mostrarMensaje() {
            $('#MensajeModal').modal({ backdrop: 'static', keyboard: false });
        }
    </script>

    <script type="text/javascript">jssor_1_slider_init();</script>
</asp:Content>
