var meetTeam = SVG("#meet-our-team").size(420, 100);

var rect = meetTeam.rect(420, 100).attr({ fill: 'transparent' });
rect.stroke({ color: 'cyan', width: 8 }).attr({
    'stroke-dashoffset': 3000,
    'stroke-dasharray': 1025,
    'stroke-linecap': 'round',
    'stroke-linejoin': 'round'
}).addClass('path');

var text = meetTeam.text('MEET THE TEAM');
text.font({ anchor: 'middle', size: 40, family: 'sans-serif' });
text.move(205, 27).rotate(180).flip('y');