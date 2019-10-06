(async () => {
    //var map = new OpenLayers.Map({
    //    target: 'map',
    //    layers: [
    //        new OpenLayers.layer.Tile({
    //            source: new ol.source.OSM()
    //        })
    //    ],
    //    view: new OpenLayers.View({
    //        center: ol.proj.fromLonLat([-73.925220, 40.755252]),
    //        zoom: 8
    //    })
    //});

    var map = new OpenLayers.Map("map");
    map.addLayer(new OpenLayers.Layer.OSM());

    epsg4326 = new OpenLayers.Projection("EPSG:4326"); //WGS 1984 projection
    projectTo = map.getProjectionObject(); //The map projection (Spherical Mercator)

    var lonLat = new OpenLayers.LonLat(0,0).transform(epsg4326, projectTo);


    var zoom = 14;
    map.setCenter(lonLat, zoom);


    console.log("Loaded map");

    async function getBenches() {
        return await fetch("/api/bench/all");
    }

    //function add_map_point(lat, lng) {
    //    vectorLayer = new ol.layer.Vector({
    //        source: new ol.source.Vector({
    //            features: [new ol.Feature({
    //                geometry: new ol.geom.Point(ol.proj.transform([parseFloat(lng), parseFloat(lat)], 'EPSG:4326', 'EPSG:3857')),
    //            })]
    //        }),
    //        style: new ol.style.Style({
    //            image: new ol.style.Icon({
    //                anchor: [0.5, 0.5],
    //                anchorXUnits: "fraction",
    //                anchorYUnits: "fraction",
    //                src: "https://upload.wikimedia.org/wikipedia/commons/e/ec/RedDot.svg"
    //            })
    //        })
    //    });
    //}
    var vectorLayer = new OpenLayers.Layer.Vector("Overlay");
    function addBenches(benches) {

        for (var i = 0; i < benches.length; i++) {
            var bench = benches[i]; 
            console.log(bench);
            console.log(bench.Seats);
            var feature = new OpenLayers.Feature.Vector(
                new OpenLayers.Geometry.Point(0,0).transform(epsg4326, projectTo),
                { description: bench.Name + "<br /> Seats:" + bench.Seats + "<br />" + bench.Description + '< br /> <a href="~/Benches/Details/' + bench.Id + '" > Details</a > ' },
                { externalGraphic: '/img/marker.png', graphicHeight: 25, graphicWidth: 21, graphicXOffset: -12, graphicYOffset: -25 }
            );
            console.log(feature);
            vectorLayer.addFeatures(feature);
        }
        map.addLayer(vectorLayer);
        console.log("Added marker layer");
    }

    var controls = {
        selector: new OpenLayers.Control.SelectFeature(vectorLayer, { onSelect: createPopup, onUnselect: destroyPopup })
    };

    function createPopup(feature) {
        feature.popup = new OpenLayers.Popup.FramedCloud("pop",
            feature.geometry.getBounds().getCenterLonLat(),
            null,
            '<div class="markerContent">' + feature.attributes.description + '</div>',
            null,
            true,
            function () { controls['selector'].unselectAll(); }
        );
        //feature.popup.closeOnMove = true;
        map.addPopup(feature.popup);
    }

    function destroyPopup(feature) {
        feature.popup.destroy();
        feature.popup = null;
    }

    map.addControl(controls['selector']);
    controls['selector'].activate();

    let response = await getBenches();
    console.log(response);
    let benches = await response.json();
    console.log(benches);
    console.log(benches[0]);
    console.log(benches[0].Seats);

    addBenches(benches); 
    //let response = await getBenches();
    //let json = await response.json();
    //let benches = await json.Benches(); 

    //function addBenches() {
    //    var vectorLayer = new ol.layer.Vector({
    //        source: new ol.source.Vector({
    //            features: [new ol.Feature({
    //                geometry: new ol.geom.Point(ol.proj.transform([parseFloat(lng), parseFloat(lat)], 'EPSG:4326', 'EPSG:3857')),
    //            })]
    //        }),
    //        style: new ol.style.Style({
    //            image: new ol.style.Icon({
    //                anchor: [0.5, 0.5],
    //                anchorXUnits: "fraction",
    //                anchorYUnits: "fraction",
    //                src: "https://upload.wikimedia.org/wikipedia/commons/e/ec/RedDot.svg"
    //            })
    //        })
    //    });
    //    map.addLayer(vectorLayer);
    //}

})();