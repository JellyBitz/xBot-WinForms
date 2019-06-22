// Increase performance with too many markers
L.Marker.addInitHook(function(){
	if(this.options.virtual){
		// setup virtualization after marker was added
		this.on('add',function(){
			this._updateIconVisibility = function() {
				if(this._map == null)
					return;
				var map = this._map,
				isVisible = map.getBounds().contains(this.getLatLng()),
				wasVisible = this._wasVisible,
				icon = this._icon,
				iconParent = this._iconParent,
				shadow = this._shadow,
				shadowParent = this._shadowParent;
				// remember parent of icon 
				if (!iconParent) {
					iconParent = this._iconParent = icon.parentNode;
				}
				if (shadow && !shadowParent) {
					shadowParent = this._shadowParent = shadow.parentNode;
				}
				// add/remove from DOM on change
				if (isVisible != wasVisible) {
					if (isVisible) {
						iconParent.appendChild(icon);
						if (shadow) {
							shadowParent.appendChild(shadow);
						}
					}else{
						iconParent.removeChild(icon);
						if (shadow) {
							shadowParent.removeChild(shadow);
						}
					}
					this._wasVisible = isVisible;
				}
			};
			// on map size change, remove/add icon from/to DOM
			this._map.on('resize moveend zoomend', this._updateIconVisibility, this);
			this._updateIconVisibility();
		}, this);
	}
});
// Silkroad map handler
var SilkroadMap = function(){  
	var map;
	// track a point at world map (x,y) or at some cave (x,y,z,region)
	var map_marker_char;
	var map_marker_char_pos;
	// current layer
	var map_layer;
	// layers supported
	var map_layer_world,
	map_layer_donwhang_1f,
	map_layer_donwhang_2f,
	map_layer_donwhang_3f,
	map_layer_donwhang_4f,
	map_layer_jangan_b1,
	map_layer_jangan_b2,
	map_layer_jangan_b3,
	map_layer_jangan_b4,
	map_layer_jangan_b5,
	map_layer_jangan_b6,
	map_layer_jobtemple_1;
	// Shapes at the map
	var map_shapes = [];
	var map_shapes_id = 0;
	// Custom pointers
	var map_pointers = [];
	// Load all map layers
	var initMapLayers = function (){
		//var b_url = 'https://jellybitz.github.io/Silkroad-Map-Viewer/images/silkroad/minimap/';
		var b_url = 'images/silkroad/minimap/';
		// zoom = 2^8 = 256x256 tiles (enough at the moment)
		map_layer_world = L.tileLayer(b_url+'{x}x{-y}.jpg',{
			attribution: '<a href="http://silkroadonline.net/">Silkroad Map</a>',center:{'x':1344,'y':-45.0844},region:0,z:0,scale:1,
			maxZoom:8,minZoom:8,errorTileUrl:b_url+'0.jpg'}); 
		map_layer_donwhang_1f = L.tileLayer(b_url+'d/dh_a01_floor01_{x}x{-y}.jpg',{
			attribution: '<a href="#">Donwhang Stone Cave [1F]</a>',center:{'x':24378,'y':-0.16},region:-32767,z:0,scale:1,
			maxZoom:8,minZoom:8,errorTileUrl:b_url+'0.jpg'});
		map_layer_donwhang_2f = L.tileLayer(b_url+'d/dh_a01_floor02_{x}x{-y}.jpg',{
			attribution: '<a href="#">Donwhang Stone Cave [2F]</a>',center:{'x':24378,'y':-0.16},region:-32767,z:0,scale:1,
			maxZoom:8,minZoom:8,errorTileUrl:b_url+'0.jpg'});
		map_layer_donwhang_3f = L.tileLayer(b_url+'d/dh_a01_floor03_{x}x{-y}.jpg',{
			attribution: '<a href="#">Donwhang Stone Cave [3F]</a>',center:{'x':24378,'y':-0.16},region:-32767,z:0,scale:1,
			maxZoom:8,minZoom:8,errorTileUrl:b_url+'0.jpg'});
		map_layer_donwhang_4f = L.tileLayer(b_url+'d/dh_a01_floor04_{x}x{-y}.jpg',{
			attribution: '<a href="#">Donwhang Stone Cave [4F]</a>',center:{'x':24378,'y':-0.16},region:-32767,z:0,scale:1,
			maxZoom:8,minZoom:8,errorTileUrl:b_url+'0.jpg'});
		map_layer_jangan_b1 = L.tileLayer(b_url+'d/qt_a01_floor01_{x}x{-y}.jpg',{
			attribution: '<a href="#">Underground Level 1 of Tomb of Qui-Shin [B1]</a>',center:{'x':23232,'y':-0.09},region:-32761,z:0,scale:1.4,
			maxZoom:8,minZoom:8,errorTileUrl:b_url+'0.jpg'});
		map_layer_jangan_b2 = L.tileLayer(b_url+'d/qt_a01_floor02_{x}x{-y}.jpg',{
			attribution: '<a href="#">Underground Level 2 of Tomb of Qui-Shin [B2]</a>',center:{'x':23424,'y':-0.09},region:-32762,z:0,scale:1.4,
			maxZoom:8,minZoom:8,errorTileUrl:b_url+'0.jpg'});
		map_layer_jangan_b3 = L.tileLayer(b_url+'d/qt_a01_floor03_{x}x{-y}.jpg',{
			attribution: '<a href="#">Underground Level 3 of Tomb of Qui-Shin [B3]</a>',center:{'x':23616,'y':-0.9},region:-32763,z:0,scale:1.4,
			maxZoom:8,minZoom:8,errorTileUrl:b_url+'0.jpg'});
		map_layer_jangan_b4 = L.tileLayer(b_url+'d/qt_a01_floor04_{x}x{-y}.jpg',{
			attribution: '<a href="#">Underground Level 4 of Tomb of Qui-Shin [B4]</a>',center:{'x':23808,'y':-0.45},region:-32764,z:0,scale:1.4,
			maxZoom:8,minZoom:8,errorTileUrl:b_url+'0.jpg'});
		map_layer_jangan_b5 = L.tileLayer(b_url+'d/qt_a01_floor05_{x}x{-y}.jpg',{
			attribution: '<a href="#">Underground Level 5 of Tomb of Qui-Shin [B5]</a>',center:{'x':24000,'y':-0.45},region:-32765,z:0,scale:1.4,
			maxZoom:8,minZoom:8,errorTileUrl:b_url+'0.jpg'});
		map_layer_jangan_b6 = L.tileLayer(b_url+'d/qt_a01_floor06_{x}x{-y}.jpg',{
			attribution: '<a href="#">Underground Level 6 of Tomb of Qui-Shin [B6]</a>',center:{'x':24192,'y':-0.45},region:-32766,z:0,scale:1.4,
			maxZoom:8,minZoom:8,errorTileUrl:b_url+'0.jpg'});
		map_layer_jobtemple_1 = L.tileLayer(b_url+'d/rn_sd_egypt1_01_{x}x{-y}.jpg',{
			attribution: '<a href="#">Job Temple</a>',center:{'x':24192,'y':-0.45},region:-32752,z:0,scale:1.4,
			maxZoom:8,minZoom:8,errorTileUrl:b_url+'0.jpg'});
	};
	// Change the current layer for another one if it's needed
	var updateMapLayer = function (layer){
		// update map
		if (map_layer && map_layer != layer){
			// remove layer and objects
			map.eachLayer(function(maplayer){
				map.removeLayer(maplayer);
			});
			// delete all shapes created
			for (var shape in map_shapes) {
				map_shapes[shape] = [];
			}
			// reset
			map_marker_char = null;
			map_layer = null;
		}
		// update the current layer
		if(!map_layer){
			map_layer = layer;
			map.addLayer(layer);
			addMapObjects();
		}
		// update character position
		if(map_marker_char_pos){
			if(map_marker_char)
				map.removeLayer(map_marker_char);
			addMapCharacter(layer);
		}
		// update all custom pointers
		for(var key in map_pointers){
			var p = map_pointers[key];
			if(p.marker){
				map.removeLayer(p.marker);
			}
			if(getLayer(p.x,p.y,p.z,p.r) == map_layer){
				p["marker"] = L.marker(SilkroadToMap(p.x,p.y,p.z,p.r),{icon:p.icon,pmIgnore:true,virtual:true}).bindPopup(p.html).addTo(map);
			}
		}
	};
	// Add clickable objects to the layer selected
	var addMapObjects = function(){
		// b_url = 'https://jellybitz.github.io/Silkroad-Map-Viewer/images/silkroad/minimap/icon/';
		var b_url = 'images/silkroad/minimap/icon/';
		// define common sizes
		var obj_npc = new L.Icon({
			iconUrl: b_url+'xy_npc.png',
			iconSize:	[11,15],
			iconAnchor:	[6,7],
			popupAnchor:[0,-7]
		});
		var obj_23px = L.Icon.extend({
			options: {
				iconSize:	[23,23],
				iconAnchor:	[12,11],
				popupAnchor:[0,-6]
			}
		});
		// Creating markers
		var obj_npc_warehouse = new obj_23px({iconUrl: b_url+'xy_warehouse.png'});
		var obj_npc_potion = new obj_23px({iconUrl: b_url+'xy_potion.png'});
		var obj_npc_stable = new obj_23px({iconUrl: b_url+'xy_stable.png'});
		var obj_npc_weapon = new obj_23px({iconUrl: b_url+'xy_weapon.png'});
		var obj_npc_armor = new obj_23px({iconUrl: b_url+'xy_armor.png'});
		var obj_npc_etc = new obj_23px({iconUrl: b_url+'xy_etc.png'});
		var obj_npc_guild = new obj_23px({iconUrl: b_url+'xy_guild.png'});
		var obj_npc_hunter = new obj_23px({iconUrl: b_url+'xy_hunter.png'});
		var obj_npc_thief = new obj_23px({iconUrl: b_url+'xy_thief.png'});
		var obj_npc_merchant = new obj_23px({iconUrl: b_url+'xy_merchant.png'});
		var obj_npc_speciality = new obj_23px({iconUrl: b_url+'xy_speciality.png'});
		var obj_npc_market_1 = new obj_23px({iconUrl: b_url+'xy_market_1.png'});
		var obj_npc_market_2 = new obj_23px({iconUrl: b_url+'xy_market_2.png'});
		var obj_npc_oction = new obj_23px({iconUrl: b_url+'xy_specialty.png'});
		var obj_npc_new = new obj_23px({iconUrl: b_url+'xy_new.png'});
		var obj_tp_gate = new obj_23px({iconUrl: b_url+'xy_gate.png'});
		var obj_tp_tel = new obj_23px({iconUrl: b_url+'map_world_icontel.png'});
		var obj_tp_ferry = new L.Icon({
			iconUrl: b_url+'xy_ferry.png',
			iconSize:	[29,25],
			iconAnchor:	[15,12],
			popupAnchor:[0,0]
		});
		var obj_tp_tahomet_gate = new L.Icon({
			iconUrl: b_url+'tahomet_gate.png',
			iconSize:	[26,28],
			iconAnchor:	[13,14],
			popupAnchor:[0,0]
		});
		var obj_npc_shamanhouse = new L.Icon({
			iconUrl: b_url+'xy_shamanhouse.png',
			iconSize:	[29,25],
			iconAnchor:	[15,12],
			popupAnchor:[0,0]
		});
		var obj_tp_dungeon = new L.Icon({
			iconUrl: b_url+'xy_dungeon.png',
			iconSize:	[31,25],
			iconAnchor:	[16,12],
			popupAnchor:[0,0]
		});
		var obj_tp_flyship = new L.Icon({
			iconUrl: b_url+'xy_flyship.png',
			iconSize:	[43,47],
			iconAnchor:	[22,23],
			popupAnchor:[0,0]
		});
		var obj_tp_fort = new L.Icon({
			iconUrl: b_url+'fort_worldmap.png',
			iconSize:	[23,45],
			iconAnchor:	[12,23],
			popupAnchor:[0,0]
		});
		var obj_tp_fort_small = new L.Icon({
			iconUrl: b_url+'fort_small_worldmap.png',
			iconSize:	[20,31],
			iconAnchor:	[10,15],
			popupAnchor:[0,0]
		});
		// Filter layer objects
		layer_npcs = [];
		switch(map_layer){
			case map_layer_world:
			// Add house icons
			// Jangan
			addMarker(obj_npc_weapon,'Blacksmith',6357,1097);
			addMarker(obj_npc_potion,'Drug Store',6513,1091);
			addMarker(obj_npc_stable,'Stable',6359,1006);
			addMarker(obj_npc_armor,'Protector Shop',6359,1061);
			addMarker(obj_npc_etc,'Grocery Shop',6514,1072);
			addMarker(obj_npc_guild,'Guild Storage',116,460);
			addMarker(obj_npc_speciality,'Specialty Shop',6524,994);
			addMarker(obj_npc_hunter,'Hunter Association',216,143);
			addMarker(obj_npc_shamanhouse,'Exorcist\'s Home',5772,1229);
			// Constantinople
			addMarker(obj_npc_warehouse,'Inn',-10600,2564);
			addMarker(obj_npc_weapon,'Blacksmith',-10679,2664);
			addMarker(obj_npc_potion,'Drug Store',-10593,2636);
			addMarker(obj_npc_stable,'Stable',-10783,2544);
			addMarker(obj_npc_armor,'Protector Shop',-10761,2589);
			addMarker(obj_npc_etc,'Grocery Shop',-10683,2501);
			addMarker(obj_npc_guild,'Guild Storage',-10527,2310);
			addMarker(obj_npc_speciality,'Specialty Shop',-10736,2497);
			addMarker(obj_npc_hunter,'Hunter Association',-10848,2689);
			// Donwhang
			addMarker(obj_npc_weapon,'Blacksmith',3590,2035);
			addMarker(obj_npc_potion,'Drug Store',3505,2028);
			addMarker(obj_npc_stable,'Stable',3590,2067);
			addMarker(obj_npc_armor,'Protector Shop',3590,2005);
			addMarker(obj_npc_etc,'Grocery Shop',3500,1989);
			addMarker(obj_npc_guild,'Guild Storage',3591,1949);
			addMarker(obj_npc_speciality,'Specialty Shop',3500,2059);
			addMarker(obj_npc_hunter,'Hunter Association',3506,2190);
			// Samarkand
			addMarker(obj_npc_warehouse,'Inn',-5113,2807);
			addMarker(obj_npc_weapon,'Blacksmith',-5209,2971);
			addMarker(obj_npc_potion,'Drug Store',-5247,2867);
			addMarker(obj_npc_stable,'Stable',-5115,2919);
			addMarker(obj_npc_armor,'Protector Shop',-5265,2921);
			addMarker(obj_npc_etc,'Grocery Shop',-5220,2818);
			addMarker(obj_npc_guild,'Guild Storage',-5153,2970);
			addMarker(obj_npc_speciality,'Specialty Shop',-5108,2852);
			addMarker(obj_npc_hunter,'Hunter Association',-5125,3010);
			// Hotan
			addMarker(obj_npc_weapon,'Blacksmith',44,88);
			addMarker(obj_npc_potion,'Drug Store',79,122);
			addMarker(obj_npc_stable,'Stable',168,-3);
			addMarker(obj_npc_armor,'Protector Shop',43,10);
			addMarker(obj_npc_etc,'Grocery Shop',77,-20);
			addMarker(obj_npc_guild,'Guild Storage',116,460);
			addMarker(obj_npc_speciality,'Specialty Shop',171,96);
			addMarker(obj_npc_hunter,'Hunter Association',216,143);

			// in progress ...
			
			break;
			case map_layer_jangan_b1:

			break;
			case map_layer_jangan_b2:

			break;
			case map_layer_jangan_b3:

			break;
			case map_layer_jangan_b4:

			break;
			case map_layer_jangan_b5:

			break;
			case map_layer_jangan_b6:

			break;
			case map_layer_donwhang_1f:

			break;
			case map_layer_donwhang_2f:

			break;
			case map_layer_donwhang_3f:

			break;
			case map_layer_donwhang_4f:

			break;
			case map_layer_jobtemple_1:

			break;
		}
	};
	// Kind minify & friendly reduced
	var addMarker = function(iconType,html,x,y,z=0,r=0){
		L.marker(SilkroadToMap(x,y,z,r),{icon:iconType,pmIgnore:true,virtual:true}).bindPopup(html).addTo(map);
	};
	// Convert a Silkroad Coord to Map CRS
	var SilkroadToMap = function (x,y,z=0,region=0){
		// Map center [X]
		x+=map_layer.options.center.x;
		// Scale & DumbFix
		x=x*1.406208/192;
		y+=Math.pow(y,2)/(25600);
		y=y*0.0052375;
		// Map center [Y] (approx)
		y=y*map_layer.options.scale;
		y+=map_layer.options.center.y;
		return [y,x];
	};
	// Convert Map LatLng to Silkroad coords
	var MapToSilkroad = function (lat,lng){
		var z=0;
		// Map center [Y] (approx)
		lat-=map_layer.options.center.y;
		lat=lat/map_layer.options.scale;
		// Scale & Inverse DumbFix
		lng = lng/1.406208*192;
		lat = lat/0.0052375;
		lat = 160*((Math.pow(lat+6400,1/2)) - 80);
		// Map center [X]
		lng-=map_layer.options.center.x;
		return [lng,lat,map_layer.options.z,map_layer.options.region];
	};
	// All data about detect the dungeon is calculated here
	var getLayer = function (x,y,z,region){
		// it's on dungeon
		if(region & 0x8000){
			switch(region){
				case -32752:
					return map_layer_jobtemple_1;
				case -32761:
					return map_layer_jangan_b1;
				case -32762:
					return map_layer_jangan_b2;
				case -32763:
					return map_layer_jangan_b3;
				case -32764:
					return map_layer_jangan_b4;
				case -32765:
					return map_layer_jangan_b5;
				case -32766:
					return map_layer_jangan_b6;
				case -32767:
					// Z VALUE UNDEFINED AT THE MOMENT
					if(z >= 0 && z < 10){
						return map_layer_donwhang_1f;
					}else if (z >= 10 && z < 20) {
						return map_layer_donwhang_2f;
					}else if (z >= 20 && z < 30 ) {
						return map_layer_donwhang_3f;
					}
					return map_layer_donwhang_4f;
			}
		}
		return map_layer_world;
	};
	// Add the character pointer
	var addMapCharacter = function (layer){
		// draw character if is at the same layer
		if(map_marker_char_pos){
			// var b_url = 'https://jellybitz.github.io/Silkroad-Map-Viewer/images/silkroad/minimap/icon/';
			var b_url = 'images/silkroad/minimap/icon/';
			var obj_char = new L.Icon({
				iconUrl: b_url+'character.png',
				iconSize:     [16,16],
				iconAnchor:   [8,16],
				popupAnchor:  [0,-16]
			});
			var p = map_marker_char_pos;
			map_marker_char = L.marker(SilkroadToMap(p[0],p[1],p[2],p[3]),{icon:obj_char}).bindPopup('Your Position').addTo(map);
		}
	};
	// Set the view using a silkroad coord
	var MoveTo = function(x,y,z=0,r=0){
		// check the layer
		updateMapLayer(getLayer(x,y,z,r));
		// set the view
		map.panTo(SilkroadToMap(x,y,z,r));
	};
	// Set the view using a silkroad coord
	var FlyTo = function(x,y,z=0,r=0){
		// check the layer
		updateMapLayer(getLayer(x,y,z,r));
		// set the view
		map.flyTo(SilkroadToMap(x,y,z,r),8);
	};
	// Copy text to clipboard
	var ToClipboard = function(text){
		var e = document.createElement('textarea');
		e.value = text;
		document.body.appendChild(e);
		e.select();
		document.execCommand('copy');
		document.body.removeChild(e);
	};
	return{
		// Initialize a map setting the view 
		init:function(id,x=113,y=12,z=0,r=0){
			// bind map by tag id
			map = L.map(id);
			// add UI controls
			// move back to Hotan or to the pointer
			L.easyButton({
				states:[{
					icon: '<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 576 576" style="vertical-align:middle"><path fill="#333" d="M444.52 3.52L28.74 195.42c-47.97 22.39-31.98 92.75 19.19 92.75h175.91v175.91c0 51.17 70.36 67.17 92.75 19.19l191.9-415.78c15.99-38.39-25.59-79.97-63.97-63.97z"/></svg>',
					title: 'Back to Your Position',
					onClick: function(){
						if(map_marker_char){
							var p = map_marker_char_pos;
							MoveTo(p[0],p[1],p[2],p[3]);
						}else{
							MoveTo(113,12);
						}
					}
				}]
			}).addTo(map);
			// Copy as text all shapes at map
			var btnCopy = L.easyButton({
				states:[{
					icon: '<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 448 512" style="vertical-align:middle; height:16px;"><path fill="#555" d="M320 448v40c0 13.255-10.745 24-24 24H24c-13.255 0-24-10.745-24-24V120c0-13.255 10.745-24 24-24h72v296c0 30.879 25.121 56 56 56h168zm0-344V0H152c-13.255 0-24 10.745-24 24v368c0 13.255 10.745 24 24 24h272c13.255 0 24-10.745 24-24V128H344c-13.2 0-24-10.8-24-24zm120.971-31.029L375.029 7.029A24 24 0 0 0 358.059 0H352v96h96v-6.059a24 24 0 0 0-7.029-16.97z"/></svg>',
					title: 'Copy Shapes to Clipboard',
					onClick: function(){
						var textFile = "";
						for (var shape in map_shapes){
							var i = 1;
							for (var id in map_shapes[shape]){
								if(i==1){
									textFile += shape+"(s):\n";
								}
								textFile += (i++)+")\n";
								switch(shape){
									case "Line":
									var d = 0, q = null;
									for (var j = 0; j < map_shapes[shape][id]._latlngs.length; j++){
										var p = MapToSilkroad(map_shapes[shape][id]._latlngs[j].lat,map_shapes[shape][id]._latlngs[j].lng);
										if(p[3]) // Cave
											textFile += "Region:"+p[3]+",";
										textFile += "X:"+Math.round(p[0])+",Y:"+Math.round(p[1])+",Z:"+Math.round(p[2])+"\n";
										if(q){ // skip the first point
											d += Math.sqrt(Math.pow(Math.round(p[0])-Math.round(q[0]),2)+Math.pow(Math.round(p[1])-Math.round(q[1]),2)+Math.pow(Math.round(p[2])-Math.round(q[2]),2));
										}
										q = p;
									}
									textFile += "Travel distance:"+d+"\n";
									break;
									case "Circle":
										var p = MapToSilkroad(map_shapes[shape][id]._latlng.lat,map_shapes[shape][id]._latlng.lng);
										if(p[3])
											textFile += "Region:"+p[3]+",";
										textFile += "X:"+Math.round(p[0])+",Y:"+Math.round(p[1])+",Z:"+Math.round(p[2])+"\n";
										textFile += "Radius:"+Math.round(map_shapes[shape][id]._radius*37/49.3)+"\n"; // A bit unprecised but enought at the moment
									break;
									case "Poly":
									for (var j = 0; j < map_shapes[shape][id]._latlngs[0].length; j++){
										var p = MapToSilkroad(map_shapes[shape][id]._latlngs[0][j].lat,map_shapes[shape][id]._latlngs[0][j].lng);
										if(p[3])
											textFile += "Region:"+p[3]+",";
										textFile += "X:"+Math.round(p[0])+",Y:"+Math.round(p[1])+",Z:"+Math.round(p[2])+"\n";
									}
									break;
								}
							}
							if (i>1){
								textFile+= "\n";
							}
						}
						ToClipboard(textFile);
					}
				}]
			});
			// Script toolbar
			L.easyButton({
				states:[{
					stateName:	'show-bar',
					icon:		'<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 576 576" style="vertical-align:middle"><path fill="#333" d="M416 320h-96c-17.6 0-32-14.4-32-32s14.4-32 32-32h96s96-107 96-160-43-96-96-96-96 43-96 96c0 25.5 22.2 63.4 45.3 96H320c-52.9 0-96 43.1-96 96s43.1 96 96 96h96c17.6 0 32 14.4 32 32s-14.4 32-32 32H185.5c-16 24.8-33.8 47.7-47.3 64H416c52.9 0 96-43.1 96-96s-43.1-96-96-96zm0-256c17.7 0 32 14.3 32 32s-14.3 32-32 32-32-14.3-32-32 14.3-32 32-32zM96 256c-53 0-96 43-96 96s96 160 96 160 96-107 96-160-43-96-96-96zm0 128c-17.7 0-32-14.3-32-32s14.3-32 32-32 32 14.3 32 32-14.3 32-32 32z"/></svg>',
					title:		'Show Script Toolbar',
					onClick: function(btn, map) {
						btn.state('hide-bar');
						map.pm.addControls({
							position:'topleft',
							drawMarker:false,
							drawPolyline:true,
							drawRectangle:false,
							drawPolygon:true,
							drawCircle:true,
							editMode:true,
							dragMode:true,
							cutPolygon:false,
							removalMode:true
						});
						btnCopy.addTo(map);
					}
				},{
					stateName:	'hide-bar',
					icon:		'<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 576 576" style="vertical-align:middle"><path fill="#555" d="M416 320h-96c-17.6 0-32-14.4-32-32s14.4-32 32-32h96s96-107 96-160-43-96-96-96-96 43-96 96c0 25.5 22.2 63.4 45.3 96H320c-52.9 0-96 43.1-96 96s43.1 96 96 96h96c17.6 0 32 14.4 32 32s-14.4 32-32 32H185.5c-16 24.8-33.8 47.7-47.3 64H416c52.9 0 96-43.1 96-96s-43.1-96-96-96zm0-256c17.7 0 32 14.3 32 32s-14.3 32-32 32-32-14.3-32-32 14.3-32 32-32zM96 256c-53 0-96 43-96 96s96 160 96 160 96-107 96-160-43-96-96-96zm0 128c-17.7 0-32-14.3-32-32s14.3-32 32-32 32 14.3 32 32-14.3 32-32 32z"/></svg>',
					title:		'Hide Script Toolbar',
					onClick: function(btn, map) {
						btn.state('show-bar');
						map.pm.addControls({
							drawPolyline:false,
							drawPolygon:false,
							drawCircle:false,
							editMode:false,
							dragMode:false,
							removalMode:false
						});
						btnCopy.remove();
					}
				}]
			}).addTo(map);
			// show coords at clicking
			map.on('dblclick', function (e){
				var p = MapToSilkroad(e.latlng.lat,e.latlng.lng);
				var c = 'X:'+Math.round(p[0])+' Y:'+Math.round(p[1]);
				if(p[3] != 0)
					c+='<br>Z:'+p[2]+' Region:'+p[3];
				//c += '<br>LAT:'+e.latlng.lat+' LNG:'+e.latlng.lng;
				L.popup().setLatLng(e.latlng).setContent(c).openOn(map);
			});
			// Keep the track of all shapes created
			map.on('pm:create',function(e){
				if(!map_shapes[e.shape]){
					map_shapes[e.shape] = [];
				}
				// easy track
				e.layer["shapeType"] = e.shape;
				e.layer["shapeId"] = map_shapes_id++;
				map_shapes[e.shape][e.layer.shapeId] = e.layer;
				// update shape if is edited
				e.layer.on('pm:edit', f => {
					map_shapes[f.target.shapeType][f.target.shapeId] = f.target;
				});
			});
			map.on('pm:remove',function(f){
				delete map_shapes[f.layer.shapeType][f.layer.shapeId];
			});
			// load layers
			initMapLayers();
			updateMapLayer(map_layer_world);
			// set initial view
			map.setView(SilkroadToMap(x,y,z,r),8);
		},
		// Set the view
		MoveTo:function(x,y,z=0,r=0){
			MoveTo(x,y,z,r);
		},
		FlyTo:function(x,y,z=0,r=0){
			FlyTo(x,y,z,r);
		},
		// Move/Remove the main pointer/character
		MovePointer:function(x,y,z=0,r=0){
			// update the position
			map_marker_char_pos = [x,y,z,r];
			FlyTo(x,y,z,r);
		},
		// Remove the main pointer/character
		RemovePointer:function(){
			map_marker_char_pos = null;
			updateMapLayer(map_layer);
		},
		AddExtraPointer(uniqueKey,servername,html,x,y,z=0,r=0){
			if(!map_pointers[uniqueKey]){
				map_pointers[uniqueKey] = [];
			}
			map_pointers[uniqueKey]["x"] = x;
			map_pointers[uniqueKey]["y"] = y;
			map_pointers[uniqueKey]["z"] = z;
			map_pointers[uniqueKey]["r"] = r;
			map_pointers[uniqueKey]["html"] = html;
			// create marker
			var b_url = 'images/silkroad/minimap/icon/';

			if(servername.startsWith("CHAR_")){
				map_pointers[uniqueKey]["icon"] = new L.Icon({
					iconUrl: b_url+'character.png',
					iconSize:     [16,16],
					iconAnchor:   [8,16],
					popupAnchor:  [0,-16]
				});
			}
			else if(servername.startsWith("COS_")){
				map_pointers[uniqueKey]["icon"] = new L.Icon({
					iconUrl: b_url+'cos.png',
					iconSize:     [16,16],
					iconAnchor:   [8,16],
					popupAnchor:  [0,-16]
				});
			}
			else if(servername.startsWith("MOB_")){
				map_pointers[uniqueKey]["icon"] = new L.Icon({
					iconUrl: b_url+'mob.png',
					iconSize:     [16,16],
					iconAnchor:   [8,16],
					popupAnchor:  [0,-16]
				});
			}
			else if(servername.startsWith("NPC_")){
				map_pointers[uniqueKey]["icon"] = new L.Icon({
					iconUrl: b_url+'xy_npc.png',
					iconSize:     [11,15],
					iconAnchor:   [6,7],
					popupAnchor:  [0,-15]
				});
			}
			// force marker update
			updateMapLayer(map_layer);
		},
		MoveExtraPointer(uniqueKey,x,y,z=0,r=0){
			if(map_pointers[uniqueKey]){
				map_pointers[uniqueKey]["x"] = x;
				map_pointers[uniqueKey]["y"] = y;
				map_pointers[uniqueKey]["z"] = z;
				map_pointers[uniqueKey]["r"] = r;
				// force marker update
				updateMapLayer(map_layer);
			}else{
				console.log("Error: uniqueKey not found.")
			}
		},
		RemoveExtraPointer(uniqueKey){
			if(map_pointers[uniqueKey]){
				if(map_pointers[uniqueKey].marker){
					map.removeLayer(map_pointers[uniqueKey].marker);
				}
				delete map_pointers[uniqueKey];
			}
		},
		RemoveAllExtraPointers(){
			map_pointers = [];
		}
	};
}();