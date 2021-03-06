"use strict";
var WorldCanvas = WorldCanvas || {};

WorldCanvas.maps = {
	none: function (res) { return []; },
	plunko: function (res) {
		var fillColor = settings.DebugMap ? "#ff0000" : "transparent";
		var items = [];
		var size = 10;
		var maxFitX = (res.width / (size * 2));
		var maxPerRow = /*((Math.random() * (maxFitX/4)) + 2)*/ 15;
		var maxFixY = (res.height / (size * 2));
		var maxPerCol = /*((Math.random() * (maxFixY)) + 2)*/ 10;
		for (var w = 0; w < maxPerRow; ++w) {
			var px = (Math.random() * res.width) + size;
			for (var h = 0; h < maxPerCol; ++h) {
				var py = (Math.random() * res.width) + size;
				items.push(WorldCanvas.Bodies.circle(px, py, size, { isStatic: true, group: 'map', name: 'map', render: { fillStyle: fillColor } }));
			}
		}

		return items;
	},
	custom: function (map, res, cb) {
		var fillColor = settings.DebugMap ? "#ff0000" : "transparent";

		console.log(`loading custom map: ${map}`);
		var data = window[`CUSTOM_MAP_${map}`] || { items: [] };
		var items = [];
		for (var x = 0; x < data.items.length; ++x) {
			var i = data.items[x];
			console.log(i);
			items.push(WorldCanvas.Bodies.rectangle(i.location.x, i.location.y, i.size.width, i.size.height, { isStatic: true, group: 'map', name: i.name, render: { fillStyle: fillColor } }));
		}
		if (cb) {
			return cb(items);
		} else {
			return;
		}
	}
};

//https://github.com/liabru/matter-js/
WorldCanvas.sprites = function () {
	WorldCanvas.Engine = Matter.Engine;
	WorldCanvas.Render = Matter.Render;
	WorldCanvas.Runner = Matter.Runner;
	WorldCanvas.Composites = Matter.Composites;
	WorldCanvas.Constraint = Matter.Constraint;
	WorldCanvas.Composite = Matter.Composite;
	WorldCanvas.Common = Matter.Common;
	WorldCanvas.MouseConstraint = Matter.MouseConstraint;
	WorldCanvas.Mouse = Matter.Mouse;
	WorldCanvas.World = Matter.World;
	WorldCanvas.Bodies = Matter.Bodies;
	WorldCanvas.Body = Matter.Body;
	WorldCanvas.active = {};
	WorldCanvas.Map = null;
	WorldCanvas.ItemsContainer = null;
	// create engine
	WorldCanvas.active.engine = WorldCanvas.Engine.create();
	WorldCanvas.active.world = WorldCanvas.active.engine.world;

	var screenRes = {
		width: settings.ScreenWidth || 1920,
		height: settings.ScreenHeight || 1080
	};

	// create renderer
	WorldCanvas.active.render = WorldCanvas.Render.create({
		element: document.body,
		engine: WorldCanvas.active.engine,
		options: {
			width: screenRes.width,
			height: screenRes.height,
			background: 'transparent',
			showAngleIndicator: false,
			wireframes: false
		}
	});
	var leftWall = WorldCanvas.Bodies.rectangle(-25, screenRes.height / 2, 50, screenRes.height + 2 * 10, { isStatic: true, render: { fillStyle: 'transparent' } });
	var rightWall = WorldCanvas.Bodies.rectangle(screenRes.width + 25, screenRes.height / 2, 50, screenRes.height + 2 * 10, { isStatic: true, render: { fillStyle: 'transparent' } });
	var bottomWall = WorldCanvas.Bodies.rectangle(screenRes.width / 2, screenRes.height + 25, screenRes.width + 2 * 10, 50, { isStatic: true, render: { fillStyle: 'transparent' } });

	WorldCanvas.Map = WorldCanvas.Composite.create({
		id: "map",
		bodies: []
	});

	WorldCanvas.ItemsContainer = WorldCanvas.Composite.create({
		id: "itemsContainer",
		bodies: []
	});


	// these static walls will not be rendered in this sprites example, see options
	WorldCanvas.World.add(WorldCanvas.active.world, [leftWall, rightWall, bottomWall]);
	WorldCanvas.World.add(WorldCanvas.active.world, WorldCanvas.Map);
	WorldCanvas.World.add(WorldCanvas.active.world, WorldCanvas.ItemsContainer);

	// if (settings.ScreenMap !== "custom") {
	// 	WorldCanvas.loadMap(settings.ScreenMap);
	// } else {
	// 	WorldCanvas.loadCustomMap(settings.ScreenMap);
	// }
	// add mouse control
	WorldCanvas.active.mouse = WorldCanvas.Mouse.create(WorldCanvas.active.render.canvas),
		WorldCanvas.active.mouseConstraint = WorldCanvas.MouseConstraint.create(WorldCanvas.active.engine, {
			mouse: WorldCanvas.active.mouse,
			constraint: {
				stiffness: 1,
				render: {
					visible: false
				}
			}
		});

	WorldCanvas.World.add(WorldCanvas.active.world, WorldCanvas.active.mouseConstraint);

	// keep the mouse in sync with rendering
	WorldCanvas.active.render.mouse = WorldCanvas.active.mouse;

	// fit the render viewport to the scene
	WorldCanvas.Render.lookAt(WorldCanvas.active.render, {
		min: { x: 0, y: 0 },
		max: { x: screenRes.width, y: screenRes.height }
	});



	WorldCanvas.Render.run(WorldCanvas.active.render);
	// create runner
	WorldCanvas.active.runner = WorldCanvas.Runner.create();
	WorldCanvas.Runner.run(WorldCanvas.active.runner, WorldCanvas.active.engine);


};

WorldCanvas.loadMap = function (map) {
	var screenRes = {
		width: settings.ScreenWidth || 1920,
		height: settings.ScreenHeight || 1080
	};
	WorldCanvas.Composite.clear(WorldCanvas.Map, false, true);

	//var mapItems = WorldCanvas.maps[map](screenRes);
	//WorldCanvas.Composite.add(WorldCanvas.Map, mapItems);
};


WorldCanvas.loadCustomMap = function (map) {
	var screenRes = {
		width: settings.ScreenWidth || 1920,
		height: settings.ScreenHeight || 1080
	};
	WorldCanvas.Composite.clear(WorldCanvas.Map, false, true);

	// WorldCanvas.maps.custom(map, screenRes, function (data) {
	// 	WorldCanvas.Composite.add(WorldCanvas.Map, data);
	// });
};

WorldCanvas.clear = function () {
	WorldCanvas.Composite.clear(WorldCanvas.ItemsContainer, true, true);
};

WorldCanvas.addObject = function (options) {
	var screenRes = {
		width: settings.ScreenWidth || 1920,
		height: settings.ScreenHeight || 1080
	};

	var count = options.count || 1;
	var items = [];
	var models = ["circle", "rectangle"];

	// if total items + count > settings.GlobalMaxItems, remove old items
	var bodies = WorldCanvas.Composite.allBodies(WorldCanvas.ItemsContainer);
	var maxDiff = (settings.GlobalMaxItems || 500) - (bodies.length + count);
	if (maxDiff < 0) {
		var offset = Math.abs(maxDiff);
		for(var x = 0; x < offset; ++x) {
			WorldCanvas.World.remove(WorldCanvas.ItemsContainer, bodies[x]);
		}
	}


	for (var index = 0; index < count && (index < (options.max || 100)); ++index) {
		var dropX = (Math.random() * (screenRes.width - 100)) + 50;
		var dropY = -((Math.random() * screenRes.height) + 90);

		var circleRadius = (((options.size || 300) * options.scale) / 2) - 5;
		var rectBound = (((options.size || 300) * options.scale));
		var model = (settings.ItemModel || "circle").toLowerCase();
		if(model.toLowerCase() === "random"){
			model = models[Math.floor(Math.random() * models.length)];
		}
		var itemOptions = {
			friction: settings.ItemFriction / 1000 /*0.005*/,
			frictionAir: settings.ItemAirFriction / 10000 /*0.0005*/,
			restitution: settings.ItemRestitution / 100 /*0.7*/,
			density: (settings.ItemDensity || 1) / 1000,
			render: {
				sprite: {
					xScale: options.scale,
					yScale: options.scale,
					texture: options.image
				}
			}
		};
		var item = null;
		if (model === "circle") {
			item = WorldCanvas.Bodies.circle(dropX, dropY, circleRadius, itemOptions);
		} else {
			item = WorldCanvas.Bodies.rectangle(dropX, dropY, rectBound, rectBound, itemOptions);
		}
		var applyHorizontal = settings.HorizontalForce / 100;
		var applyVertical = settings.VerticalForce / 100;
		var force = dropX > ((screenRes.height - 90) / 2) ? -(applyHorizontal) : applyHorizontal;

		WorldCanvas.Body.applyForce(
			item,
			{ x: item.position.x, y: item.position.y },
			{ x: force, y: applyVertical }
		);

		var delay = Math.floor((Math.random() * 2000) + 100);
		var ttl = options.ttl || 0;
		if (ttl && ttl > 0) {
			setTimeout(function (x) {
				WorldCanvas.World.remove(WorldCanvas.ItemsContainer, x);
			}, (ttl * 1000) + delay, item);
		}
		items.push(item);
	}
	WorldCanvas.World.add(WorldCanvas.ItemsContainer, items);


};

WorldCanvas.sprites();
