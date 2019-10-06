(async () => {

    var benchesfull = await getBenches(); //we only fetch the full list once, store it in memory and handle all filtering from this copy
    var benches = benchesfull; //this is the working copy 
    var map;
    var vectorLayer;
    var controls;
    function drawMap() {
        map = new OpenLayers.Map("map");
        map.addLayer(new OpenLayers.Layer.OSM());

        epsg4326 = new OpenLayers.Projection("EPSG:4326"); //WGS 1984 projection
        projectTo = map.getProjectionObject(); //The map projection (Spherical Mercator)

        var lonLat = new OpenLayers.LonLat(0, 0).transform(epsg4326, projectTo);


        var zoom = 14;
        map.setCenter(lonLat, zoom);
        console.log("Loaded map");

        vectorLayer = new OpenLayers.Layer.Vector("Overlay");
        for (var i = 0; i < benches.length; i++) {
            var bench = benches[i];
            var long = parseInt(bench.Latitude);
            var lat = parseInt(bench.Longitude);
            var feature = new OpenLayers.Feature.Vector(
                new OpenLayers.Geometry.Point(long, lat).transform(epsg4326, projectTo),
                { description: bench.Name + "<br /> Seats:" + bench.Seats + "<br />" + bench.Description + '<br /> <a href="Benches/Details/' + bench.Id + '" > Details</a > ' },
                { externalGraphic: '/img/marker.png', graphicHeight: 25, graphicWidth: 21, graphicXOffset: -12, graphicYOffset: -25 }
            );
            console.log(feature);
            vectorLayer.addFeatures(feature);
        }
        map.addLayer(vectorLayer);
        console.log("Added marker layer");
        controls = {
            selector: new OpenLayers.Control.SelectFeature(vectorLayer, { onSelect: createPopup, onUnselect: destroyPopup })
        };
        map.addControl(controls['selector']);
        controls['selector'].activate();
    }
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

    async function getBenches(){
        var response = await fetch("/api/bench/all");
        return await response.json();
    }
    function populateList() {
        var table = document.getElementById("table");
        var header = "";
        header += "<tr>";
        header += "<th>Name</th>";
        header += "<th>Seats</th>";
        header += "<th>Description</th>";
        header += "<th>Longitude</th>";
        header += "<th>Latitude</th>";
        header += "<th>Rating Average</th>";
        header += "<th>Added by</th>";
        header += "<th></th></tr>";
        var benchList = "";
        if (benches.length == 0) benchList = "<p>No benches found</p>";

        for (var i = 0; i < benches.length; i++) { // just an ugly for loop to spit out all of this as html
            bench = benches[i];
            benchList += "<tr>";
            benchList += "<td>" + bench.Name + "</td>";
            benchList += "<td>" + bench.Seats + "</td>";
            var description = bench.Description.split(" ");
            if (description.length <= 10) {
                benchList += "<td>" + bench.Description + "</td>";
            }
            else {
                var temp = "<td>";
                for (var j = 0; j < 10; j++) { // another ugly for loop if the description is longer than 10 words
                    temp += description[j] + " ";
                }
                temp += "... </td>";
                benchList += temp;
            }
            benchList += "<td>" + bench.Longitude + "</td>";
            benchList += "<td>" + bench.Latitude + "</td>";
            if (bench.RatingAverage === 0) {
                benchList += "<td>No ratings</td>";
            }
            else {
                var ratingAverage = Math.round(parseFloat(bench.RatingAverage) * 10) / 10 + "";
                if (ratingAverage.length == 1) ratingAverage += ".0";
                benchList += "<td>" + ratingAverage + "</td>";
            }
            benchList += "<td>" + bench.CreatorName + "</td>";
            benchList += '<td><a href="/benches/details/' + bench.Id + '"> Details </a></td>';
            benchList += "</tr>";
        }

        table.innerHTML = header + benchList;
    }

    drawMap();
    populateList();
    document.getElementById("filter").addEventListener("click", filterList);

    function filterList() {
        var result = [];
        var min = parseInt(document.getElementById("min").value);
        var max = parseInt(document.getElementById("max").value);
        for (var i = 0; i < benchesfull.length; i++) {
            var bench = benchesfull[i];
            if (bench.Seats >= min && bench.Seats <= max) {
                result.push(bench);
            }
        }
        console.log(result);
        benches = result;
        document.getElementById("map").innerHTML = ""; //throw out old map and list
        drawMap();
        document.getElementById("table").innerHTML = "";
        populateList();
    }





})();